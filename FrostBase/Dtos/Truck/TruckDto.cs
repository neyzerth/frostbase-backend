public class TruckDto
{
    public string Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string LicensePlate { get; set; }
    public StateDto State { get; set; }

    public static TruckDto FromModel(Truck? t)
    {
        if (t == null) return null!;
        return new TruckDto
        {
            Id = t.Id,
            Brand = t.Brand,
            Model = t.Model,
            LicensePlate = t.LicensePlate,
            State = StateDto.FromModel(global::StateTruck.Get(t.IDStateTruck))       
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