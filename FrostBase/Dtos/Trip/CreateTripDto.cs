public class CreateTripDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartHour { get; set; }
    public TimeSpan EndHour { get; set; }
    public bool State { get; set; }
    
}