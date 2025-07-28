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

    public static Time GenerateOrderDeliverTime(Location startLocation, DateTime startTime, OrderDto order )
    {
        DateTime endTime;
        try
        {
            Console.WriteLine("== Store "+ order.Store.Name +"==");
            var nextStore = order.Store.Location;
            var routeApi = Osrm.GetRoute(startLocation, nextStore);
            double traveledTime = GenerateRandomTime(routeApi.Result.Duration, .10, .20);
            double stayedTime = GenerateRandomTime(15 * 60, .30, 1.00);
            
            endTime = startTime.AddSeconds(traveledTime + stayedTime);
            
            Console.WriteLine("Traveled time:\t" + TimeSpan.FromSeconds(traveledTime));
            Console.WriteLine("Stayed time:\t" + TimeSpan.FromSeconds(stayedTime));
            Console.WriteLine("Start:  \t" + startTime.TimeOfDay);
            Console.WriteLine("End time:\t" + endTime.TimeOfDay);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error generating order deliver time: "+e.Message);
        }

        return new Time(startTime, endTime);
    }
    
    public static double GenerateRandomTime(double seconds, double percAdvantage, double percMaxTime)
    {
        Random random = new Random();
        double time = seconds - (seconds * percAdvantage);
        double extraTime = random.NextDouble() * (seconds * percMaxTime);
        return time + extraTime;
    }
}