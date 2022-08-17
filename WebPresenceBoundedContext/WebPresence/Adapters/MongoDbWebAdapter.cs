using MongoDB.Driver;

namespace WebPresence.Adapters;

public class MongoDbWebAdapter
{
    private readonly IMongoCollection<CourseEntity> _coursesCollection;
    private readonly IMongoCollection<OfferingEntity> _offeringsCollection;
    private readonly IMongoCollection<EnrollmentEntity> _enrollmentsCollection;

    public MongoDbWebAdapter(string connectionString)
    {
        var client = new MongoClient(connectionString); // from constructor
        var database = client.GetDatabase("web_db");
        _coursesCollection = database.GetCollection<CourseEntity>("courses");
        _offeringsCollection = database.GetCollection<OfferingEntity>("offerings");
        _enrollmentsCollection = database.GetCollection<EnrollmentEntity>("enrollments");
    }

    public IMongoCollection<CourseEntity> Courses { get { return _coursesCollection; } }
    public IMongoCollection<OfferingEntity> Offerings { get { return _offeringsCollection; } }

    public IMongoCollection<EnrollmentEntity> Enrollments { get { return _enrollmentsCollection; } }

}
