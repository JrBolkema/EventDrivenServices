using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebPresence.Domain;

public class EnrollmentEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public string OfferingId { get; set; } = string.Empty;
    public string CourseId { get; set; } = string.Empty;
    public DateTime Created { get; set; }



    public EnrollmentStatus EnrollmentStatus { get; set; } = EnrollmentStatus.Pending;

    public string? DeniedReason { get; set; }
}

public enum EnrollmentStatus
{
    Pending,
    Approved,
    Denied,
    WaitListed
}