public class NoStoresRouteDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<int> DeliverDays { get; set; }
    public UserDto Driver { get; set; }
    public static NoStoresRouteDto FromModel(Route r)
    {
        return new NoStoresRouteDto
        {
            Id = r.Id,
            Name = r.Name,
            DeliverDays = r.DeliverDays,
            Driver = UserDto.FromModel(UserApp.Get(r.IDUser)),
        };
    }

    public static List<NoStoresRouteDto> FromModel(List<Route> routes)
    {
        List<NoStoresRouteDto> routesDto = new List<NoStoresRouteDto>();
        foreach (Route r in routes)
        {
            routesDto.Add(FromModel(r));
        }
        return routesDto;
    }
}
