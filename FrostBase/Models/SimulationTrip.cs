using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

public class SimulationTrip
{
    public SimulationTrip(Trip simulatedTrip)
    {
        SimulatedTrip = simulatedTrip;
        Inserted = false;       
    }

    //collection of the trips
    private static IMongoCollection<SimulationTrip> _simTripColl = 
        MongoDbConnection.GetCollection<SimulationTrip>("SimulationTrips");

    #region attributes

    [BsonElement("simulatedTrip")]
    public Trip SimulatedTrip { get; set; }
    
    [BsonElement("inserted")]
    public bool Inserted { get; set; }
    
    [BsonElement("ordersInserted")]
    public bool OrdersInserted { get; set; }

    #endregion
    
    #region simulation
    
    public static List<Trip> CheckSimulationsTrips(DateTime date)
    {  
        var simTrips =  _simTripColl.Find(t =>
            !t.OrdersInserted && t.SimulatedTrip.StartTime.Date <= date).ToList();
        
        var newTrips = new List<Trip>();

        foreach (var sim in simTrips)
        {
            var trip = sim.SimulatedTrip;
            var newOrders = new List<TripOrder>();

            string stateTrip = trip.EndTime > date ? "IP" : "CP";
            
            var newTrip = new Trip
            {
                Id = trip.Id,
                StartTime = trip.StartTime,
                EndTime = trip.EndTime <= date ? trip.EndTime : null,
                IDTruck = trip.IDTruck,
                IDRoute = trip.IDRoute,
                IDStateTrip = trip.EndTime <= date ? "CP" : "IP",
                IDUser = trip.IDUser,
                Orders = newOrders,
            };
            
            //check each order
            foreach (var order in sim.SimulatedTrip.Orders)
            {
                //if the order does not start yet, continue with the next
                if (order.StartTime <= date) continue;
                
                var newOrd = new TripOrder
                {
                    IDOrder = order.IDOrder,
                    StartTime = order.StartTime,
                    EndTime = order.EndTime <= date ? order.EndTime : null,
                };
                newOrders.Add(newOrd);
            }
            newTrips.Add(newTrip);
        }
        return newTrips;
    }    

    #endregion
}