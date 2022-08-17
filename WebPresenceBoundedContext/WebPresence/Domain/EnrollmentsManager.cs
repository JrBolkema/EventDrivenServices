using MongoDB.Driver;

namespace WebPresence.Domain;

public class EnrollmentsManager
{
    private readonly MongoDbWebAdapter _adapter;
    private readonly DaprPubSubAdapter _dapr;

    public EnrollmentsManager(MongoDbWebAdapter adapter, DaprPubSubAdapter dapr)
    {
        _adapter = adapter;
        _dapr = dapr;
    }

    public async Task<bool> CheckForAlreadyEnrolledAsync(string offeringId, string userId)
    {
        // TODO: What about declined?
        var filter = Builders<EnrollmentEntity>.Filter.Where(e => e.UserId == userId && e.OfferingId == offeringId) ;

        var any = await _adapter.Enrollments.CountDocumentsAsync(filter);
        return any> 0;
    }

    public async Task<OfferingDetails> GetOfferingDetails(string offeringId)
    {
        var offeringsFilter = Builders<OfferingEntity>.Filter.Where(o => o.Id == offeringId);

        var offering = await _adapter.Offerings.Find(offeringsFilter).SingleOrDefaultAsync();

        var courseFilter = Builders<CourseEntity>.Filter.Where(c => c.Id == offering.Course);

        var course = await _adapter.Courses.Find(courseFilter).SingleOrDefaultAsync();

        return new OfferingDetails
        {
            Course = course,
            Offering = offering
        };
    }

    public async Task CreateEnrollmentAsync(string userId, string offeringId, string courseId)
    {
        var enrollment = new EnrollmentEntity
        {
            OfferingId = offeringId,
            UserId = userId,
            Created = DateTime.Now,
            EnrollmentStatus = EnrollmentStatus.Pending,
            CourseId = courseId

        };

        await _adapter.Enrollments.InsertOneAsync(enrollment);
        await _dapr.NotifyOfEnrollmentAsync(enrollment);
        
    }

    public async Task<EnrollmentsSummary> GetEnrollmentsForUserAsync(string userId)
    {
        var enrollmentFilter = Builders<EnrollmentEntity>.Filter.Where(e => e.UserId == userId);

        var enrollmentProjection = Builders<EnrollmentEntity>.Projection.Expression(e =>
            new EnrollmentSummaryItem { EnrolledDate = e.Created, EnrollmentId = e.Id.ToString(), Status = e.EnrollmentStatus, CourseId = e.CourseId }
        );

        var enrollments = await _adapter.Enrollments.Find(enrollmentFilter).Project(enrollmentProjection).ToListAsync();

        foreach(var enrollment in enrollments)
        {
            enrollment.CourseTitle = await _adapter.Courses.Find(c => c.Id == enrollment.CourseId).Project(e => e.Title).SingleOrDefaultAsync();
        }

        return new EnrollmentsSummary { Enrollments = enrollments.OrderBy(e => e.EnrolledDate).ToList() };

    }
}
