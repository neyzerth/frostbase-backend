public class CreateRouteDto
{
    public string Name { get; set; }
    public string IDCreatedBy { get; set; }
    public List<int> DeliverDays { get; set; }
    public List<RouteStore> stores { get; set; }
}