public class TripDto
{
    public string Id { get; set; }
    public Time TripTime { get; set; }
    public StateTrip State { get; set; }
    public TruckDto Truck { get; set; }
    public UserDto Driver { get; set; }
    public RouteDto Route { get; set; }
    public List<TripOrderDto> Orders { get; set; }

    public static TripDto FromModel(Trip t)
    {
        return new TripDto
        {
            Id = t.Id,
            State = StateTrip.Get(t.IDStateTrip),
            Route = RouteDto.FromModel(global::Route.Get(t.IDRoute)),
            Orders = TripOrderDto.FromModel(t.Orders)
        };
    }

    public static List<TripDto> FromModel(List<Trip> trips)
    {
        List<TripDto> tripsDto = new List<TripDto>();
        foreach (Trip t in trips)
        {
            tripsDto.Add(FromModel(t));
        }
        return tripsDto; 
    }
}

public class TripOrderDto
{
    public OrderDto Order { get; set; }
    public Time OrderTime { get; set; }

    public static TripOrderDto FromModel(TripOrder to)
    {
        return new TripOrderDto
        {
            Order = OrderDto.FromModel(global::Order.Get(to.IDOrder)),
            OrderTime = new Time
            {
                StartTime = to.StartTime,
                EndTime = to.EndTime,
                TotalTime = to.EndTime.HasValue ? 
                    (to.EndTime.Value - to.StartTime) : null
            }
        };
    }
    
    public static List<TripOrderDto> FromModel(List<TripOrder> tos)
    {
        List<TripOrderDto> tosDto = new List<TripOrderDto>();
        foreach (TripOrder to in tos)
        {
            tosDto.Add(FromModel(to));
        }
        return tosDto;   
    }
}

public class Time
{
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public TimeSpan? TotalTime { get; set; }
}
