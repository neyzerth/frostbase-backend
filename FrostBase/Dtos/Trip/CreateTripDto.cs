public class CreateTripDto
{
    public string Id { get; set; }
    public DateTime Date { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string IDRoute { get; set; }
    public string IDDriver { get; set; }
    public string IDTruck { get; set; }
    public string IDState { get; set; }
}