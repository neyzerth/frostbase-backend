using MongoDB.Bson.Serialization.Attributes;


public class CreateAlert
{   
    public int Id { get; set; }
    
    public string State { get; set; }
    
    public DateTime Date { get; set; }
    
    public decimal DetectedValue { get; set; }
    
    public string AlertType { get; set; }
    
    public string IDTruck { get; set; }
}