using MongoDB.Bson.Serialization.Attributes;

namespace WebPresence.Domain;

public class OfferingEntity
{
    [BsonElement("_id")]
    public string Id { get; set; } = String.Empty;

    public string Course { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }
    public string StartTime { get; set; } = string.Empty;
    public string EndTime { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string DeliveryMethod { get; set; } = string.Empty;

    public bool HasSeatsAvailable { get; set; }
}
