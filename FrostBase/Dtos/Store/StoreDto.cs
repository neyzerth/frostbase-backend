public class StoreDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public Location Location { get; set; }

    public static StoreDto FromModel(Store s)
    {
        if(s == null) return new StoreDto();
        return new StoreDto
        {
            Id = s.Id,
            Name = s.Name,
            Phone = s.Phone,
            Location = new Location
            {
                Address = s.Location,
                Latitude = s.Latitude,
                Longitude = s.Longitude
            }       
        };
    }

    public static List<StoreDto> FromModel(List<Store> stores)
    {
        List<StoreDto> storesDto = new List<StoreDto>();
        foreach (Store s in stores)
        {
            storesDto.Add(FromModel(s));
        }
        return storesDto;  
    }
}

public class Location
{
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}