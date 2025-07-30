public class AlertDto
{
    public string Id { get; set; }
    public DateTime Date { get; set; }
    public bool State { get; set; }
    public ReadingDto Reading { get; set; }
    public StateDto AlertType { get; set; }
    
    public static AlertDto FromModel(Alert a)
    {
        return new AlertDto
        {
            Id = a.Id,
            Date = a.Date,
            State = a.State,
            Reading = ReadingDto.FromModel(global::Reading.Get(a.IDReading)),
            AlertType = StateDto.FromModel(global::AlertType.Get(a.IDAlertType))
        };
    }

    public static List<AlertDto> FromModel(List<Alert> alerts)
    {
        List<AlertDto> alertsDto = new List<AlertDto>();
        foreach (Alert a in alerts)
        {
            alertsDto.Add(FromModel(a));
        }
        return alertsDto;   
    }
    
}