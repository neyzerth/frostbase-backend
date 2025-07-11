public class CreateRouteDto
{   
    public int Id { get; set; }
    public string Name { get; set; }
    public List<int> DeliverDays { get; set; }
    public int IDUser { get; set; }
}