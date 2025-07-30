public class TripDto
{
    public string Id { get; set; }
    public Time TripTime { get; set; }
    public StateTrip State { get; set; }
    public TruckDto Truck { get; set; }
    public UserDto Driver { get; set; }
    public NoStoresRouteDto Route { get; set; }
    public List<TripOrderDto> Orders { get; set; }

    public static TripDto FromModel(Trip t)
    {
        try
        {
            return new TripDto
            {
                Id = t.Id,
                TripTime = new Time
                {
                    StartTime = t.StartTime,
                    EndTime = t.EndTime
                },
                State = StateTrip.Get(t.IDStateTrip),
                Truck = TruckDto.FromModel(global::Truck.Get(t.IDTruck)),
                Driver = UserDto.FromModel(UserApp.Get(t.IDUser)),
                Route = NoStoresRouteDto.FromModel(global::Route.Get(t.IDRoute)),
                Orders = TripOrderDto.FromModel(t.Orders)
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
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

public class TripOrderDto : OrderDto
{
    public Time OrderTime { get; set; }

    public TripOrderDto()
    { }

    public TripOrderDto(OrderDto o)
    {
        Id = o.Id;
        Date = o.Date;
        DeliverDate = o.DeliverDate;
        CreatedBy = o.CreatedBy;
        Store = o.Store;
        State = o.State;
        OrderTime = new Time();
    }

    public static TripOrderDto FromModel(TripOrder to)
    {
        var tripOrder = new TripOrderDto(OrderDto.FromModel(global::Order.Get(to.IDOrder)));
        tripOrder.OrderTime = new Time
        {
            StartTime = to.StartTime,
            EndTime = to.EndTime
        };
        
        return tripOrder;       
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
    public Time(DateTime startTime, DateTime endTime)
    {
        StartTime = startTime;
        EndTime = endTime;
    }

    public Time()
    {
        StartTime = DateTime.Now;
        EndTime = null;   
    }

    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public TimeSpan? TotalTime => Total(); 

    public TimeSpan? Total()
    {
        TimeSpan endTime = EndTime.HasValue? 
            EndTime.Value.TimeOfDay : DateTime.Now.TimeOfDay;
        
        return endTime.Subtract(StartTime.TimeOfDay);
    }
}
