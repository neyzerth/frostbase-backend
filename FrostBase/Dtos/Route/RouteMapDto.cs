public class RouteMapDto : RouteDto
{
    List<Location> Waypoints { get; set; }

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
    public static RouteMapDto FromModel(Route route)
    {
        RouteDto routeDto = FromModel(route);;
        
        return new RouteMapDto(routeDto, route.GetMapRoute());
    }
    
    public static List<RouteMapDto> FromModel(List<Route> route)
    {
        var routes = new List<RouteMapDto>();
        foreach (var r in route)
        {
            routes.Add(FromModel(r));
        }
        return routes;
    }
}