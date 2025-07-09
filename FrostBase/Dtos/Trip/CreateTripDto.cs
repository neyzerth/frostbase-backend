public class CreateTripDto
{
    public string Id { get; set; }
    public DateTime Date { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string IdRoute { get; set; }
    public string IdDriver { get; set; }
    public string IdTruck { get; set; }
    public string State { get; set; }
}