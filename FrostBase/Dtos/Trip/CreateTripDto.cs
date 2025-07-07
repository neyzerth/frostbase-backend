public class CreateTripDto
{
    public string Id { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartHour { get; set; }
    public TimeSpan EndHour { get; set; }
    public string State { get; set; }
}