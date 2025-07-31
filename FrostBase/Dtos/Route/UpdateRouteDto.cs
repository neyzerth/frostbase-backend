public class UpdateRouteDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string IDCreatedBy { get; set; }
    public List<int> DeliverDays { get; set; }
    public List<RouteStore> Stores { get; set; }

}