public class RouteView : JsonResponse
{
    public Route Route { get; set; }
    
    public static RouteView GetResponse(Route route, int status = 0)
    {
        return new RouteView
        {
            Status = status,
            Route = route
        };
    }
}
