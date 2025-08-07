public class RouteMapDto : RouteDto
{
    public List<Location> Waypoints { get; set; }

    public RouteMapDto(RouteDto routeDto, List<Location> waypoints)
    {
        Waypoints = waypoints;
        Id = routeDto.Id;
        Name = routeDto.Name;
        DeliverDays = routeDto.DeliverDays;
        Driver = routeDto.Driver;
        Stores = routeDto.Stores;
        Waypoints = waypoints;
    }
    public new static RouteMapDto FromModel(Route route)
    {
        var routeDto = RouteDto.FromModel(route);;
        
        return new RouteMapDto(routeDto, route.GetMapRoute());
    }
    
    public new static List<RouteMapDto> FromModel(List<Route> route)
    {
        var routes = new List<RouteMapDto>();
        foreach (var r in route)
        {
            routes.Add(FromModel(r));
        }
        return routes;
    }
}