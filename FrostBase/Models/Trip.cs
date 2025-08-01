using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

public class Trip
{
    #region statement
    
    //mongo statements
    private static IMongoCollection<Trip> _tripColl = 
        MongoDbConnection.GetCollection<Trip>("Trips");
    
    private static IMongoCollection<Trip> _tripSimulation = 
        MongoDbConnection.GetCollection<Trip>("TripsSimulation");
    
    #endregion

    #region properties

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("start_time")]
    public DateTime StartTime { get; set; }

    [BsonElement("end_time")]   
    public DateTime? EndTime { get; set; }
    
    [BsonElement("IDTruck")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IDTruck { get; set; }
    
    [BsonElement("IDUserDriver")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IDUser { get; set; }
    
    [BsonElement("IDRoute")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IDRoute { get; set; }
    
    [BsonElement("IDStateTrip")]  
    public string IDStateTrip { get; set; }

    [BsonElement("orders")]
    public List<TripOrder> Orders { get; set; }

    #endregion

    #region constructors

    public Trip()
    {
        Id = ObjectId.GenerateNewId().ToString();
        StartTime = DateTime.Now;
        IDTruck = "";
        IDUser = "";
        IDRoute = "";
        IDStateTrip = "IP";
        Orders = new List<TripOrder>();
    }

    public Trip(StartTripDto c)
    {
        Id = ObjectId.GenerateNewId().ToString();
        StartTime = c.StartDate;
        IDTruck = c.IDTruck;
        IDUser = c.IDDriver;
        IDRoute = c.IDRoute;
        IDStateTrip = "IP";
        Orders = new List<TripOrder>();
    }

    #endregion
    
    #region db methods
    
    public static List<Trip> Get() 
    {
        return _tripColl.Find(t => true).ToList();
    }

    public static Trip Get(string id)
    {
        return _tripColl.Find(t => t.Id == id).FirstOrDefault();
    }

    public static Trip Insert(Trip t)
    {
        try
        {
            if (string.IsNullOrEmpty(t.Id))
                t.Id = ObjectId.GenerateNewId().ToString();
            
            if (t.Orders == null)
                t.Orders = new List<TripOrder>();
            
            if(Truck.Get(t.IDTruck) == null) 
                throw new Exception("Truck not founded with id "+ t.IDTruck);
            
            if(UserApp.Get(t.IDUser) == null) 
                throw new Exception("User not founded with id "+ t.IDUser);
            
            if(Route.Get(t.IDRoute) == null)
                throw new Exception("Route not founded with id "+ t.IDRoute);
            
            TripLog.Insert(t, t.StartTime);
            _tripColl.InsertOne(t);
            return t;
        }
        catch (Exception e)
        {
            Console.WriteLine("Trip insert: "+e);
            throw new Exception("Error inserting trip "+t.Id+": "+e.Message);
        }
    }
    public static Trip Insert(CreateTripDto c)
    {
        Trip t = new Trip
        {
            Id = c.Id, 
            StartTime = c.StartTime,
            EndTime = c.EndTime,
            IDTruck = c.IDTruck,
            IDUser = c.IDDriver,
            IDRoute = c.IDRoute,
            IDStateTrip = c.IDState,
            
        };
        
        Truck truck = Truck.Get(t.IDTruck);
        truck.IDStateTruck = "IR";
        Truck.Update(truck);
        
        TruckLog.Insert(truck, t.StartTime);
        
        return Insert(t);
    }
    public static Trip Insert(StartTripDto c)
    {
        Trip t = new Trip()
        {
            StartTime = DateTime.Now,
            IDTruck = c.IDTruck,
            IDUser = c.IDDriver,
            IDRoute = c.IDRoute,
            IDStateTrip = "IP",
            Orders = new List<TripOrder>()
        };
        
        
        return Insert(t);
    }
    
    #endregion
    
    #region class methods
    
    public static Trip UpdateEndTime(string idTrip)
    {
        try
        {
            Trip trip = _tripColl.Find(t => t.Id == idTrip).FirstOrDefault();
            
            DateTime endTime = DateTime.Now;
            TimeSpan totalTime = endTime.TimeOfDay.Subtract(trip.StartTime.TimeOfDay);
            var filter = Builders<Trip>.Filter.Eq(t => t.Id, trip.Id);
            var update = Builders<Trip>.Update
                .Set(t => t.EndTime, endTime)
                .Set(t => t.IDStateTrip, "compl");
                
            var newTrip =  _tripColl.FindOneAndUpdate(filter, update,
                    new FindOneAndUpdateOptions<Trip>
                    { ReturnDocument = ReturnDocument.After }
                );
            
            TripLog.Insert(newTrip, endTime);
            
            return newTrip;
        }
        catch (Exception e)
        {
            Console.WriteLine("Trip insert: "+e);
            throw new Exception("Error updating trip endtime: "+e.Message);
        }
    }
    public static Trip StartOrder(string tripId, string orderId)
    {
        try
        {
            TripOrder newOrder = new TripOrder
            {
                IDOrder = orderId,
                StartTime = DateTime.Now,
                EndTime = null
            };
            
            // Crear un filtro para encontrar el viaje por su ID
            var filter = Builders<Trip>.Filter.Eq(t => t.Id, tripId);
            
            var pushUpdate = Builders<Trip>.Update.Push(t => t.Orders, newOrder);
            
            var result = _tripColl.FindOneAndUpdate(filter, pushUpdate,
                new FindOneAndUpdateOptions<Trip>
                    { ReturnDocument = ReturnDocument.After }
                );
            
            TripLog.Insert(result, newOrder.StartTime);

            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine("Trip insert: "+e);
            throw new Exception("Error starting order");
        }
    }
    
    public static Trip EndOrder(string tripId, string orderId)
    {
        try
        {
            Trip trip = _tripColl.Find(t => t.Id == tripId).FirstOrDefault();
            
            if (trip == null)
                return null; // El viaje no existe
            
            if (trip.Orders == null || trip.Orders.All(o => o.IDOrder != orderId))
                return null; // La orden específica no existe
            
            var filter = Builders<Trip>.Filter.And(
                Builders<Trip>.Filter.Eq(t => t.Id, trip.Id),
                Builders<Trip>.Filter.ElemMatch(t => t.Orders, o => o.IDOrder == orderId)
            );
            
            var update = Builders<Trip>.Update
                .Set(t => t.Orders[0].EndTime, DateTime.Now);
                
            var result = _tripColl.FindOneAndUpdate(filter, update,
                new FindOneAndUpdateOptions<Trip>
                    { ReturnDocument = ReturnDocument.After }
                );
            
            TripLog.Insert(result, result.Orders[0].EndTime);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine("Trip insert: "+e);
            throw new Exception("Error ending order");
        }
    }
    

    #endregion
    
    #region simulator

    public static Trip Simulate(DateTime? date = null)
    {
        date ??= DateTime.Now;
        Trip trip = GenerateStartTrip(date);
        
        trip.GenerateOrders();
        
        trip.GenerateEndTimeTrip();

        return SimulationTrip.Insert(trip).SimulatedTrip;
    }

    public static Trip GenerateStartTrip(DateTime? date = null)
    {
        date??= DateTime.Now;
        
        Random random = new Random();
        
        //get random route (that its valid for today)
        List<Route> routes = Route.GetByDate(date.Value);
        
        if (routes.Count == 0) throw new FrostbaseException("No routes for " + date.Value.Date, 1, 404);
        
        Route route = routes[random.Next(0, routes.Count-1)];
        
        //get a random user
        List<Truck> trucks = Truck.GetAvailable();
        if (trucks.Count == 0) throw new FrostbaseException("No trucks available",1,404);
        
        Truck truck = trucks[random.Next(0, trucks.Count-1)];
        
        StartTripDto t = new StartTripDto
        {
            IDTruck = truck.Id,
            IDDriver = route.IDUser,
            IDRoute = route.Id,
            StartDate = date.Value,
        };

        var newTrip = new Trip(t);
        TripLog.Insert(newTrip, newTrip.StartTime);;

        return new Trip(t);
    }

    public void GenerateEndTimeTrip()
    {
        //get the last order
        if(Orders.Count <= 0 )
            throw new Exception("No orders in trip");
        TripOrder lastOrder = Orders.Last();
        StoreDto s = OrderDto.FromModel(Order.Get(lastOrder.IDOrder)).Store;
        Location lastLocation = s.Location;

        Location lalaBase = new Location
        {
            Latitude = 32.45900929216648,
            Longitude = -116.97966765227373
        };

        double timeToReturn = Osrm.Get(lastLocation, lalaBase).Duration;
        
        DateTime lastOrderTime = lastOrder.EndTime.Value;
        
        this.EndTime = lastOrderTime.AddSeconds(timeToReturn);
        
        this.IDStateTrip = "CP";
        
        TripLog.Insert(this, this.EndTime);
        
    }
    
    public void GenerateOrders()
    {
        Orders = new List<TripOrder>();
        
        //starter location (Grupo Lala / 32.45900929216648, -116.97966765227373 )
        var startLocation = new Location
            { Latitude = 32.45900929216648, Longitude = -116.97966765227373 };
        
        //the first order startTime is the same as the trip startTime
        var orderStartTime =  StartTime;
        
        //use dto to get the store location in one request
        List<OrderDto> orders = OrderDto.FromModel(Order.GetByRoute(IDRoute, StartTime));
        
        Console.WriteLine("== GENERATE TIMES ===============");
        foreach (OrderDto order in orders)
        {
            TripLog.Insert(this, orderStartTime);
            //generate times
            var times = TripOrder.GenerateOrderDeliverTime(startLocation, orderStartTime, order);
            
            //dto to model
            var orderModel = new TripOrder
            {
                IDOrder = order.Id,
                StartTime = times.StartTime,
                EndTime = times.EndTime
            };
            
            //add to log
            OrderLog.Insert(new Order(order), times.StartTime);
            order.State.Id = "DO";
            OrderLog.Insert(new Order(order), times.EndTime.Value);
            
            Orders.Add(orderModel);
            TripLog.Insert(this, times.EndTime.Value);
            //the next startTime and store is the previous endTime trip
            startLocation = order.Store.Location;
            orderStartTime = times.EndTime.Value;
            
        }
        Console.WriteLine("== END GENERATE TIMES ===============");
        
    }


    #endregion
}
