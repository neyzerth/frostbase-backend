using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

public class SimulationTrip
{

    //collection of the trips
    private static IMongoCollection<SimulationTrip> _simTripColl = 
        MongoDbConnection.GetCollection<SimulationTrip>("TripsSimulation");

    #region attributes

    [BsonElement("simulatedTrip")]
    public Trip SimulatedTrip { get; set; }
    
    [BsonElement("inserted")]
    public bool Inserted { get; set; }
    
    [BsonElement("ordersInserted")]
    public bool OrdersInserted { get; set; }

    #endregion

    #region constructors
    
    public SimulationTrip()
    {
        SimulatedTrip = new Trip();
        Inserted = false;       
        OrdersInserted = false;       
    }
    
    public SimulationTrip(Trip simulatedTrip)
    {
        SimulatedTrip = simulatedTrip;
        Inserted = false;
        OrdersInserted = false;
    }

    #endregion

    #region db methods

    public static SimulationTrip Insert(SimulationTrip simTrip)
    {
        try
        {
            if(string.IsNullOrEmpty(simTrip.SimulatedTrip.Id))
                simTrip.SimulatedTrip.Id = ObjectId.GenerateNewId().ToString();
            _simTripColl.InsertOne(simTrip);
            return simTrip;       
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error inserting simulation trip: "+e.Message);
        }
    }

    public static SimulationTrip Insert(Trip simulatedTrip)
    {
        try
        {
            return Insert(new SimulationTrip(simulatedTrip));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error inserting simulation trip of "+simulatedTrip.Id+": "+e.Message);
        }
    }

    #endregion
    
    #region simulation
    
    public static List<Trip> CheckSimulationsTrips(DateTime? date)
    {  
        date??= DateTime.Now;
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
                IDUser = trip.IDUser,
                IDStateTrip = stateTrip,
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