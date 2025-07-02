public class RouteListView : JsonResponse
{
    public List<Route> Routes { get; set; }

    public static RouteListView GetResponse(List<Route> routes, int status)
    {
        return new RouteListView
        {
            Status = status,
            Routes = routes
        };
    }
}
