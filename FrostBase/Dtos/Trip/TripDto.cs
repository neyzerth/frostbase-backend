public class TripDto
{
    public string Id { get; set; }
    public DateOnly Date { get; set; }
    public Time TripTime { get; set; }
    public StateTrip State { get; set; }
    public RouteDto Route { get; set; }
    public List<TripOrderDto> Orders { get; set; }

    public static TripDto FromModel(Trip t)
    {
        return new TripDto
        {
            Id = t.Id,
            Date = DateOnly.FromDateTime(t.Date),
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

public class Time
{
    public TimeSpan StartHour { get; set; }
    public TimeSpan? EndHour { get; set; }
    public TimeSpan TotalTime { get; set; }
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
                StartHour = to.TimeStart.TimeOfDay,
                EndHour = to.TimeEnd.HasValue ? to.TimeEnd.Value.TimeOfDay : TimeSpan.Zero,
                TotalTime = to.TimeEnd.HasValue ? 
                    (to.TimeEnd.Value - to.TimeStart) : DateTime.Now - to.TimeStart
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