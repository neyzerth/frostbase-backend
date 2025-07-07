public class RouteDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public UserDto Driver { get; set; }
    public List<RouteStoreDto> Stores { get; set; }
}

public class RouteStoreDto
{
    public StoreDto Store { get; set; }
    public int Sequence { get; set; }
}