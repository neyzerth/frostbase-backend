using MongoDB.Bson.IO;

public class StartTripDto
{
    public string Id { get; set; }
    public DateTime Date { get; set; }
    public string IDRoute { get; set; }
    public TimeSpan StartHour { get; set; }
    public List<Order> Orders { get; set; }
    public string State { get; set; }
}