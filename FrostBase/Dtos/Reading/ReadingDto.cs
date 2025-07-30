public class ReadingDto
{
    public string Id { get; set; }
    public DateTime Date { get; set; }
    public bool DoorState {get; set;}
    public double Temp { get; set; }
    public int PercHumidity {get; set;}
    public _Location Location { get; set; }
    public TruckDto Truck { get; set; }
    
    public static ReadingDto FromModel(Reading r)
    {
        return new ReadingDto
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
            },
            Truck = TruckDto.FromModel(global::Truck.Get(r.IDTruck)) 
            
        };
    }
    
    public static List<ReadingDto> FromModel(List<Reading> readings)
    {
        List<ReadingDto> readingsDto = new List<ReadingDto>();
        foreach (Reading r in readings)
        {
            readingsDto.Add(FromModel(r));
        }
        return readingsDto;
    }
}

public class _Location
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

