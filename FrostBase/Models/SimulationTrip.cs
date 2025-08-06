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

    public static List<Simulation> SimulateByRange(DateTime start, DateTime end)
    {
        DateTime date = start.Date;
        DateTime endDate = end.Date;
        var simulations = new List<Simulation>();
        while (date <= endDate)
        {
            try
            {
                var simulation = new Simulation(date);
                Console.WriteLine($">>>>>>> SIMULATING {simulation.Date}");

                simulation.Orders = Order.GenerateOrders(date);
                simulation.Trips = SimulationTrip.SimulateByDate(date);
                simulations.Add(simulation);
            }
            catch (RouteWithOrdersNotFoundException e)
            {
                Console.WriteLine($"No orders for {date} found");
            }
            catch (StoresWithNoOrdersNotFoundException e)
            {
                Console.WriteLine("All stores ordered yet");
            }
            catch (NoOrdersForRouteException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (FrostbaseException e)
            {
                Console.WriteLine($"Error simulating {date}: {e.Message}");
            }
            
            date = date.AddDays(1);
        }

        return simulations;
    }
    
    
    public static Trip Simulate(DateTime? date = null)
    {
        Random random = new Random();
        
        //get random route (that its valid for today)
        List<Route> routes = Route.GetByDate(date.Value);
        
        if (routes.Count == 0) throw new RouteWithOrdersNotFoundException(date.Value);
        
        Route route = routes[random.Next(0, routes.Count-1)];
        
        return Simulate(route, date);
    }

    private static Trip Simulate(Route route, DateTime? date = null)
    {
        date ??= DateTime.Now;
        
        //the same date but set at 7am
        DateTime newDate = date.Value.Date.AddHours(7);
        //trip starts between 7am and 9am
        var simulatedTime = CalculateRandomMinutes(newDate, 0, 120);
        
        Trip trip = Trip.GenerateStartTrip(route, simulatedTime);
        var truck = Truck.Get(trip.IDTruck);
        truck.IDStateTruck = "IR";
        if(trip.StartTime <= DateTime.Now)
            Truck.Update(truck,trip.StartTime);
        
        trip.GenerateOrders();
        
        trip.GenerateEndTimeTrip();
        truck.IDStateTruck = "AV";
        
        if(trip.EndTime.Value <= DateTime.Now)
            Truck.Update(truck, trip.EndTime.Value);

        return trip;
    }
    public static List<Trip> SimulateByDate(DateTime? date = null)
    {
        date ??= DateTime.Now;

        var startOfDay = date.Value.Date;
        var endOfDay = startOfDay.AddDays(1).AddTicks(-1);

        var simulations = _simTripColl.Find(
            s => s.SimulatedTrip.StartTime >= startOfDay && s.SimulatedTrip.StartTime <= endOfDay
        ).ToList();


        if (simulations.Count > 0)
        {
            Console.WriteLine($"{date.Value.Date} already simulated, returning {simulations.Count} trips");
            return simulations.ToList().ConvertAll(s => s.SimulatedTrip);
        }
        
        //get a random route (that it's valid for today)
        List<Route> routes = Route.GetWithOrders(date.Value);
        
        if (routes.Count == 0) throw new RouteWithOrdersNotFoundException(date.Value);

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
        DateTime newDate = date;
        var random = rnd.NextDouble() * (maxMin - minMin);
        
        return newDate.AddMinutes(random);
    }
    
    
    public static List<Trip> CheckSimulationsTrips()
    {  
        DateTime date = DateTime.Now;
        //check trips simulated that isn't inserted yet
        var simTrips =  _simTripColl.Find(t =>
            !t.OrdersInserted && t.SimulatedTrip.StartTime <= date).ToList();
        
        var newTrips = new List<Trip>();

        foreach (var sim in simTrips)
        {
            var trip = sim.SimulatedTrip;
            var newOrders = new List<TripOrder>();

            bool inserted = false;
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
                if (order.StartTime > date)
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
                ordersInserted = newOrd.EndTime != null; 
                newOrders.Add(newOrd);
            }
            inserted = newTrip.EndTime != null;
            
            sim.Inserted = inserted;
            sim.OrdersInserted = ordersInserted;

            Update(sim);
            newTrips.Add(newTrip);
        }
        return Trip.Upsert(newTrips);
    }    

    #endregion
}

public class Simulation
{
    public DateOnly Date { get; set; }
    public List<Trip> Trips { get; set; }
    public List<Order> Orders { get; set; }

    public Simulation(DateTime date)
    {
        Date = new DateOnly(date.Year, date.Month, date.Day);
        Trips = new List<Trip>();
        Orders = new List<Order>();
    }
    public Simulation()
    {
        Trips = new List<Trip>();
        Orders = new List<Order>();
    }
}