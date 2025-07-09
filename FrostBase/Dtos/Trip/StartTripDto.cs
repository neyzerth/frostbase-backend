using MongoDB.Bson.IO;

public class StartTripDto
{
    public string Id { get; set; }
    public string IDRoute { get; set; }
    public DateTime StartHour { get; set; }
    public List<TripOrder> Orders { get; set; }
    public string State { get; set; }

}