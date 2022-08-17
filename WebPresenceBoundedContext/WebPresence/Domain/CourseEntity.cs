using MongoDB.Bson.Serialization.Attributes;

namespace WebPresence.Domain;

public class CourseEntity
{
    [BsonElement("_id")]
    public string Id { get; set; } = String.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    public int NumberOfDays { get; set; }
}
