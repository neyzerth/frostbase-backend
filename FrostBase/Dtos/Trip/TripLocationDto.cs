public class TripLocationDto
{
    public Truck Truck { get; set; }
    public string IDTrip { get; set; }
    public Coordinates Location { get; set; }
    public TimeSpan ReadingTime { get; set; }

    public static TripLocationDto FromModel(TripLocation t)
    {
        var dto = new TripLocationDto()
        {
            Truck = t.Truck,
            IDTrip = t.IDTrip,
            Location = new Coordinates()
            {
                Latitude = t.Latitude,
                Longitude = t.Longitude
            },
            ReadingTime = t.Date.TimeOfDay
        };
        return dto;
    }

    public static List<TripLocationDto> FromModel(List<TripLocation> trips)
    {
        var tripsDto = new List<TripLocationDto>();
        foreach (var tripLocation in trips)
        {
            tripsDto.Add(FromModel(tripLocation));       
        }
        return tripsDto;   
    }
}

public class Coordinates
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}