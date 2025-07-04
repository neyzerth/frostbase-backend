using FrostBase.Models.Alert;
using MongoDB.Bson.Serialization.Attributes;

namespace FrostBase.Dtos.Alert;

public class CreateAlert
{   
    [BsonId]
    public int Id { get; set; }
    
    [BsonElement("state")]
    public string State { get; set; }
    
    [BsonElement("date")]
    public DateTime Date { get; set; }
    
    [BsonElement("detectedValue")]
    public decimal DetectedValue { get; set; }
    
    [BsonElement("alert_type")]
    public string AlertType { get; set; }
    
    [BsonElement("IDtruck")]
    public string IDTruck { get; set; }
}