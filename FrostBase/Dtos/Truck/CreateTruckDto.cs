using MongoDB.Bson.Serialization.Attributes;

public class CreateTruckDto
{
    [BsonId]
    public int Id { get; set; }
    [BsonElement("brand")]
    public string Brand { get; set; }
    [BsonElement("model")]
    public string Model { get; set; }
    [BsonElement("licensePlate")]
    public string LicensePlate { get; set; }
    [BsonElement("state")]
    public bool State { get; set; }
}