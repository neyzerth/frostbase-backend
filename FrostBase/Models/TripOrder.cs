using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class TripOrder
{
    [BsonElement("IDOrder")] 
    [BsonRepresentation(BsonType.ObjectId)]
    public string IDOrder { get; set; }
    
    [BsonElement("start_time")] 
    public DateTime StartTime { get; set; }
    
    [BsonElement("end_time")] 
    public DateTime? EndTime { get; set; }

    public static List<TripOrder> GenerateOrders(DateTime startTime, List<OrderDto> orders)
    {
        List<TripOrder> tripOrders = new List<TripOrder>();
        //starter location (Grupo Lala / 32.45900929216648, -116.97966765227373 )
        var start = new Location
        { Latitude = 32.45900929216648, Longitude = -116.97966765227373 };
        
        //first order startTime is the same as the trip startTime
        var nextOrder = new TripOrderDto
        { OrderTime = new Time{ StartTime = startTime, EndTime = startTime}, };
        
        
        Console.WriteLine("== GENERATE TIMES ===============");
        foreach (OrderDto order in orders)
        {
            nextOrder.Order = order;
            nextOrder = TripOrderDto.FromModel(GenerateOrderDeliverTime(start, nextOrder));
            var orderModel = new TripOrder
            {
                IDOrder = nextOrder.Order.Id,
                StartTime = nextOrder.OrderTime.StartTime,
                EndTime = nextOrder.OrderTime.EndTime
            };
            tripOrders.Add(orderModel);
            
            start = order.Store.Location;
            
            nextOrder = new TripOrderDto
            { OrderTime = new Time
            {
                StartTime = nextOrder.OrderTime.StartTime,
                EndTime = nextOrder.OrderTime.EndTime
            }};
        }
        Console.WriteLine("== END GENERATE TIMES ===============");
        
        
        return tripOrders;
    }

    public static TripOrder GenerateOrderDeliverTime(Location start, TripOrderDto tripOrd)
    {
        try
        {
            Console.WriteLine("== Store "+ tripOrd.Order.Store.Name +"==");
            var nextStore = tripOrd.Order.Store.Location;
            var routeApi = Osrm.GetRoute(start, nextStore);
            double traveledTime = GenerateRandomTime(routeApi.Result.Duration, .10, .20);
            double stayedTime = GenerateRandomTime(15 * 60, .30, 1.00);
            
            tripOrd.OrderTime.EndTime = tripOrd.OrderTime.StartTime.AddSeconds(traveledTime + stayedTime);
            
            Console.WriteLine("Traveled time:\t" + TimeSpan.FromSeconds(traveledTime));
            Console.WriteLine("Stayed time:\t" + TimeSpan.FromSeconds(stayedTime));
            Console.WriteLine("Start:  \t" + tripOrd.OrderTime.StartTime.TimeOfDay);
            Console.WriteLine("End time:\t" + tripOrd.OrderTime.EndTime.Value.TimeOfDay);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
            
            
        return new TripOrder
        {
            IDOrder = tripOrd.Order.Id,
            StartTime = tripOrd.OrderTime.StartTime,
            EndTime = tripOrd.OrderTime.EndTime.Value,
        };
    }
    
    public static double GenerateRandomTime(double seconds, double percAdvantage, double percMaxTime)
    {
        Random random = new Random();
        double time = seconds - (seconds * percAdvantage);
        double extraTime = random.NextDouble() * (seconds * percMaxTime);
        return time + extraTime;
    }
}