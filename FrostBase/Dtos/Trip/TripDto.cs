public class TripDto
{
    public string Id { get; set; }
    public DateOnly Date { get; set; }
    public Time TripTime { get; set; }
    public StateTripDto State { get; set; }
    public RouteDto Route { get; set; }
    public List<OrderDto> Orders { get; set; }
}

public class Time
{
    public TimeSpan StartHour { get; set; }
    public TimeSpan EndHour { get; set; }
    public TimeSpan TotalTime { get; set; }
}

public class StateTripDto
{
    public string Id { get; set; }
    public string Description { get; set; }
}

public class TripOrderDto
{
    public OrderDto Order { get; set; }
    public Time OrderTime { get; set; }
}