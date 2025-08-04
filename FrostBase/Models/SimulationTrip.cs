using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

public class SimulationTrip
{

    //collection of the trips
    private static readonly IMongoCollection<SimulationTrip> _simTripColl = 
        MongoDbConnection.GetCollection<SimulationTrip>("TripsSimulation");

    #region attributes
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

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
            Console.WriteLine("Simulation trip insert: "+e);
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
            Console.WriteLine("Simulation trip insert: "+e);
            throw new Exception("Error inserting simulation trip of "+simulatedTrip.Id+": "+e.Message);
        }
    }
    
    
    public static List<SimulationTrip> InsertMany(List<SimulationTrip> simTrips)
    {
        try
        {
            foreach (var simTrip in simTrips)
            {
                if(string.IsNullOrEmpty(simTrip.SimulatedTrip.Id))
                    simTrip.SimulatedTrip.Id = ObjectId.GenerateNewId().ToString();
            }
            
            _simTripColl.InsertMany(simTrips);
            return simTrips;       
        }
        catch (Exception e)
        {
            Console.WriteLine("Simulation trip insert: "+e);
            throw new Exception("Error inserting simulation trip: "+e.Message);
        }
    }
    
    public static SimulationTrip Update(SimulationTrip updatedTrip)
    {
        try
        {
            var filter = Builders<SimulationTrip>.Filter.Eq(u => u.Id, updatedTrip.Id);
            var update = Builders<SimulationTrip>.Update
                .Set(u => u.Inserted, updatedTrip.Inserted)
                .Set(u => u.OrdersInserted, updatedTrip.OrdersInserted);
            

            var options = new FindOneAndUpdateOptions<SimulationTrip>
            {
                ReturnDocument = ReturnDocument.After
            };

            return _simTripColl.FindOneAndUpdate(filter, update, options);
        }
        catch (Exception e)
        {
            Console.WriteLine("user insert: "+e);
            throw new Exception("Error updating user: " + e.Message);
        }
    }


    #endregion
    
    #region simulation
    
    
    public static Trip Simulate(DateTime? date = null)
    {
        Random random = new Random();
        
        //get random route (that its valid for today)
        List<Route> routes = Route.GetByDate(date.Value);
        
        if (routes.Count == 0) throw new FrostbaseException("No routes for " + date.Value.Date, 1, 404);
        
        Route route = routes[random.Next(0, routes.Count-1)];
        
        return Simulate(route, date);
    }
    public static Trip Simulate(Route route, DateTime? date = null)
    {
        date ??= DateTime.Now;
        
        //the same date but set at 7am
        DateTime newDate = date.Value.Date.AddHours(7);
        //trip starts between 7am and 8am
        var simulatedTime = CalculateRandomMinutes(newDate, 0, 60);
        
        Trip trip = Trip.GenerateStartTrip(route, simulatedTime);
        
        trip.GenerateOrders();
        
        trip.GenerateEndTimeTrip();

        return trip;
    }
    public static List<Trip> SimulateByDate(DateTime? date = null)
    {
        date ??= DateTime.Now;
        
        //get random route (that its valid for today)
        List<Route> routes = Route.GetWithOrders(date.Value);
        
        if (routes.Count == 0) throw new FrostbaseException("No routes with orders for " + date.Value.Date, 1, 404);

        var trips = new List<Trip>();
        var simulation = new List<SimulationTrip>();
        foreach (var route in routes)
        {
            var trip = Simulate(route, date);
            trips.Add(trip);
            simulation.Add(new SimulationTrip(trip));
        }

        try
        {
            InsertMany(simulation);
            CheckSimulationsTrips();
            return trips;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new FrostbaseException("Error inserting simulation trips");
        }
    }

    public static DateTime CalculateRandomMinutes(DateTime date, int minMin, int maxMin)
    {
        Random rnd = new Random();
        DateTime newDate = date.Date;
        var random = rnd.NextDouble() * (maxMin - minMin) + minMin;
        
        return newDate.AddMinutes(random);
    }
    
    
    public static List<Trip> CheckSimulationsTrips()
    {  
        DateTime date = DateTime.Now;
        //check trips simulated that isn't inserted yet
        var simTrips =  _simTripColl.Find(t =>
            !t.Inserted && t.SimulatedTrip.StartTime.Date <= date).ToList();
        
        var newTrips = new List<Trip>();

        foreach (var sim in simTrips)
        {
            var trip = sim.SimulatedTrip;
            var newOrders = new List<TripOrder>();

            bool inserted = true;
            bool ordersInserted = true;

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
                if (order.StartTime < date)
                {
                    ordersInserted = false;
                    continue;
                }
                
                var newOrd = new TripOrder
                {
                    IDOrder = order.IDOrder,
                    StartTime = order.StartTime,
                    EndTime = order.EndTime < date ? order.EndTime : null,
                };
                ordersInserted = newOrd.EndTime == null ? false : true; 
                newOrders.Add(newOrd);
            }
            inserted = newTrip.EndTime == null ? false : true;
            
            sim.Inserted = inserted;
            sim.OrdersInserted = ordersInserted;

            Update(sim);
            newTrips.Add(newTrip);
        }
        return Trip.Upsert(newTrips);
    }    

    #endregion
}