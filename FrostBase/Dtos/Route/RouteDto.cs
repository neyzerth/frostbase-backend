public class RouteDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<int> DeliverDays { get; set; }
    public UserDto Driver { get; set; }
    public List<RouteStoreDto> Stores { get; set; }

    public static RouteDto FromModel(Route r)
    {
        return new RouteDto
        {
            Id = r.Id,
            Name = r.Name,
            DeliverDays = r.DeliverDays,
            Driver = UserDto.FromModel(UserApp.Get(r.IDUser)),
            Stores = RouteStoreDto.FromModel(r.Stores)
        };
    }

    public static List<RouteDto> FromModel(List<Route> routes)
    {
        List<RouteDto> routesDto = new List<RouteDto>();
        foreach (Route r in routes)
        {
            routesDto.Add(FromModel(r));
        }
        return routesDto;
    }
}

public class RouteStoreDto
{
    public StoreDto Store { get; set; }
    public int Sequence { get; set; }

    public static RouteStoreDto FromModel(RouteStore rs)
    {
        return new RouteStoreDto
        {
            Store = StoreDto.FromModel(global::Store.Get(rs.IDStore)),
            Sequence = rs.Sequence
        };
    }

    public static List<RouteStoreDto> FromModel(List<RouteStore> routes)
    {
        List<RouteStoreDto> routesDto = new List<RouteStoreDto>();
        foreach (RouteStore r in routes)
        {
            routesDto.Add(FromModel(r));
        }
        return routesDto; 
    }
}