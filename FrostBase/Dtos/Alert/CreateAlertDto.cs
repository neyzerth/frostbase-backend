using MongoDB.Bson.Serialization.Attributes;


public class CreateAlertDto
{   
    
    public bool State { get; set; }
    
    public DateTime? Date { get; set; }
    
    public string AlertType { get; set; }
    
    public string Reading { get; set; }
}