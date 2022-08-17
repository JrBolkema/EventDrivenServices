using MongoDB.Bson;
using MongoDB.Driver;

namespace WebPresence.Domain;

public class CourseManager
{
    private readonly MongoDbWebAdapter _adapter;

    public CourseManager(MongoDbWebAdapter adapter)
    {
        _adapter = adapter;
    }

    public async Task<List<CourseSummaryItem>> GetCourseListAsync()
    {
        var projection = Builders<CourseEntity>.Projection.Expression(c => new CourseSummaryItem(c.Id, c.Title, c.NumberOfDays));
        return await _adapter.Courses.Find(new BsonDocument()).Project(projection).ToListAsync();
    }

    public async Task<CourseDetails> GetCourseDetailsAsync(string courseId)
    {
        var courseFilter = Builders<CourseEntity>.Filter.Where(c => c.Id == courseId);
        var offeringsFilter = Builders<OfferingEntity>.Filter.Where(o => o.Course == courseId);
        var response = new CourseDetails();

        response.Course = await _adapter.Courses.Find(courseFilter).SingleOrDefaultAsync();
        response.Offerings = await _adapter.Offerings.Find(offeringsFilter)
            
            .ToListAsync();

        response.Offerings = response.Offerings.Where(o => o.StartDate >= DateTime.Now)
            .OrderBy(o => o.StartDate).ToList();
        return response;
    }
}
