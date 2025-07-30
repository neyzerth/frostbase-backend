public class UpdateAlertDto
{
    public string Id { get; set; }
    public DateTime Date { get; set; }
    public bool State { get; set; }
    public string IDReading { get; set; }
    public string IDAlertType { get; set; }
}