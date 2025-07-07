public class TruckDto
{
    public string Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string LicensePlate { get; set; }
    public StateTruck State { get; set; }

    public static TruckDto FromModel(Truck t)
    {
        return new TruckDto
        {
            Id = t.Id,
            Brand = t.Brand,
            Model = t.Model,
            LicensePlate = t.LicensePlate,
            State = StateTruck.Get(t.State)
        };
    }

    public static List<TruckDto> FromModel(List<Truck> trucks)
    {
        List<TruckDto> trucksDto = new List<TruckDto>();
        foreach (Truck t in trucks)
        {
            trucksDto.Add(FromModel(t));
        }
        return trucksDto;   
    }
}