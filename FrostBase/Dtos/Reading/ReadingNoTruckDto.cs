public class ReadingNoTruckDto
{
    public string Id { get; set; }
    public DateTime Date { get; set; }
    public bool DoorState {get; set;}
    public double Temp { get; set; }
    public int PercHumidity {get; set;}
    public _Location Location { get; set; }

    public static ReadingNoTruckDto FromModel(Reading r)
    {
        var dto = new ReadingNoTruckDto
        {
            Id = r.Id,
            Date = r.Date,
            DoorState = r.DoorState,
            Temp = r.Temperature,
            PercHumidity = r.PercHumidity,
            Location = new _Location
            {
                Latitude = r.Latitude,
                Longitude = r.Longitude
            }
        };
        return dto;
    }

    public static List<ReadingNoTruckDto> FromModel(List<Reading> readings)
    {
        List<ReadingNoTruckDto> readingsDto = new List<ReadingNoTruckDto>();
        foreach (Reading r in readings)
        {
            readingsDto.Add(FromModel(r));
        }
        return readingsDto;
    }
}