public class StoreDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public Location Location { get; set; }
}

public class Location
{
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}