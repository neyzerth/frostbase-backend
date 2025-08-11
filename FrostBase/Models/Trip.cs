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
    
    private static readonly IMongoCollection<Order> _orderColl =
        MongoDbConnection.GetCollection<Order>("Orders");

    
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
    
    public static Trip Upsert(Trip t)
    {
        try
        {
            if (string.IsNullOrEmpty(t.Id))
                t.Id = ObjectId.GenerateNewId().ToString();

            if (t.Orders == null)
                t.Orders = new List<TripOrder>();

            if (Truck.Get(t.IDTruck) == null)
                throw new FrostbaseException("Truck not found with id " + t.IDTruck);

            if (UserApp.Get(t.IDUser) == null)
                throw new FrostbaseException("User not found with id " + t.IDUser);

            if (Route.Get(t.IDRoute) == null)
                throw new FrostbaseException("Route not found with id " + t.IDRoute);

            TripLog.Insert(t, t.StartTime);

            var filter = Builders<Trip>.Filter.Eq(x => x.Id, t.Id);

            var options = new ReplaceOptions { IsUpsert = true };

            _tripColl.ReplaceOne(filter, t, options);

            return t;
        }
        catch (Exception e)
        {
            Console.WriteLine("Trip upsert: " + e);
            throw new Exception("Error upserting trip " + t.Id + ": " + e.Message);
        }
    }
    
    public static List<Trip> Upsert(List<Trip> trips)
    {
        try
        {
            foreach (var t in trips)
            {
                if (string.IsNullOrEmpty(t.Id))
                    t.Id = ObjectId.GenerateNewId().ToString();

                if (t.Orders == null)
                    t.Orders = new List<TripOrder>();

                if (Truck.Get(t.IDTruck) == null)
                    throw new FrostbaseException("Truck not found with id " + t.IDTruck);

                if (UserApp.Get(t.IDUser) == null)
                    throw new FrostbaseException("User not found with id " + t.IDUser);

                if (Route.Get(t.IDRoute) == null)
                    throw new FrostbaseException("Route not found with id " + t.IDRoute);

                TripLog.Insert(t, t.StartTime);

                var filter = Builders<Trip>.Filter.Eq(x => x.Id, t.Id);

                var options = new ReplaceOptions { IsUpsert = true };

                _tripColl.ReplaceOne(filter, t, options);
            }

            return trips;
        }
        catch (Exception e)
        {
            Console.WriteLine("Trip upsert: " + e);
            throw new Exception("Error upserting trips  " + e.Message);
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
            // Buscar el viaje
            Trip trip = _tripColl.Find(t => t.Id == idTrip).FirstOrDefault();
            if (trip == null) throw new Exception("Trip not found");

            // Calcular hora de finalización
            DateTime endTime = DateTime.Now;
            TimeSpan totalTime = endTime.TimeOfDay.Subtract(trip.StartTime.TimeOfDay);

            // Actualizar el viaje
            var filter = Builders<Trip>.Filter.Eq(t => t.Id, trip.Id);
            var update = Builders<Trip>.Update
                .Set(t => t.EndTime, endTime)
                .Set(t => t.IDStateTrip, "CP"); // Estado cambiado a Completado

            var updatedTrip = _tripColl.FindOneAndUpdate(filter, update,
                new FindOneAndUpdateOptions<Trip> { ReturnDocument = ReturnDocument.After });

            // Registrar en log
            TripLog.Insert(updatedTrip, endTime);

            // Cambiar órdenes asociadas a estado "DO"
            if (updatedTrip != null && updatedTrip.Orders?.Any() == true)
            {
                var orderIds = updatedTrip.Orders.Select(o => o.IDOrder).ToList();

                var orderFilter = Builders<Order>.Filter.In(o => o.Id, orderIds);
                var orderUpdate = Builders<Order>.Update
                    .Set(o => o.IDStateOrder, "DO")
                    .Set(o => o.DeliverDate, DateTime.UtcNow);

                _orderColl.UpdateMany(orderFilter, orderUpdate);
            }

            return updatedTrip;
        }
        catch (Exception e)
        {
            Console.WriteLine("Trip update error: " + e);
            throw new Exception("Error updating trip endtime: " + e.Message);
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
            Console.WriteLine("Trip update: "+e);
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
            
            // if (trip.Orders == null || trip.Orders.All(o => o.IDOrder != orderId))
            //     return null; // La orden específica no existe
            
            var filter = Builders<Trip>.Filter.And(
                Builders<Trip>.Filter.Eq(t => t.Id, trip.Id),
                Builders<Trip>.Filter.ElemMatch(t => t.Orders, o => o.IDOrder == orderId)
            );
            
            var update = Builders<Trip>.Update
                .Set("orders.$.end_time", DateTime.Now);

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

    public static Trip GenerateStartTrip(Route route, DateTime? date = null)
    {
        date??= DateTime.Now;
        
        //get a driver info
        var driver = UserApp.Get(route.IDUser);
        
        StartTripDto t = new StartTripDto
        {
            IDTruck = driver.IDTruckDefault,
            IDDriver = route.IDUser,
            IDRoute = route.Id,
            StartDate = date.Value,
        };

        var newTrip = new Trip(t);
        TripLog.Insert(newTrip, newTrip.StartTime);;

        return newTrip;
    }

    public void GenerateEndTimeTrip()
    {
        //get the last order
        if(Orders.Count <= 0 )
            throw new Exception("No orders in trip");
        TripOrder lastOrder = Orders.Last();
        StoreDto s = OrderDto.FromModel(Order.Get(lastOrder.IDOrder)).Store;
        Location lastLocation = new Location(s.Location);

        Location lalaBase = Location.LalaBase();

        var returnRoute = Osrm.Get(lastLocation, lalaBase);
        
        DateTime lastOrderTime = lastOrder.EndTime.Value;
        
        this.EndTime = lastOrderTime.AddSeconds(returnRoute.Duration);
        
        this.IDStateTrip = "CP";

        var baseReading = Reading.BaseReading(lastLocation, lastOrderTime, IDTruck);
        //Console.WriteLine("== LALA BASE READINGS");
        var readings = Reading.GenerateTripReadings(baseReading, lastOrderTime, EndTime.Value, lalaBase, 10 );
        TripLog.Insert(this, this.EndTime);
        // //Reading.Insert(baseReading);
        Reading.Insert(readings);
        var lastInsert = Reading.Insert(
                Reading.LastReading(
                    readings.Last(), 
                    readings.Last().Date.AddSeconds(10), 
                    IDTruck)
            );
        //Console.WriteLine($"-- LALA BASE READING: {lastInsert.Latitude},{lastInsert.Longitude} | {lastInsert.Date}");
    }
    
    public void CompleteOrders()
    {
        Orders = new List<TripOrder>();
        
        //starter location (Grupo Lala / 32.45900929216648, -116.97966765227373 )
        var startLocation = Location.LalaBase();
        
        //the first order startTime is the same as the trip startTime
        var orderStartTime =  StartTime;
        
        var route  = Route.Get(IDRoute);
        
        //order by te secuence
        route.Stores = route.Stores.OrderBy(r => r.Sequence).ToList();
        
        //Console.WriteLine("== GENERATE TIMES ===============");
    
        foreach (var store in route.Stores)
        {
            var deliverOrder = Store.GetDeliverOrder(store.IDStore, StartTime);
            if (deliverOrder == null)
                continue;
            var order = OrderDto.FromModel(deliverOrder);
            TripLog.Insert(this, orderStartTime);
            //generate times
            var times = TripOrder.GenerateOrderDeliverTime(startLocation, orderStartTime, order, IDTruck);
            
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
            // var update = Order.Update(new Order(order));
            // if(update.IDStateOrder == "PO")
            //     Console.WriteLine("NO UPDATED ORDER: "+order.Id + "|"+ order.DeliverDate);
            
            Orders.Add(orderModel);
            TripLog.Insert(this, times.EndTime.Value);
            //the next startTime and store is the previous endTime trip
            startLocation = new Location(order.Store.Location);
            orderStartTime = times.EndTime.Value;
            
        }

        if (Orders.Count <= 0)
            throw new NoOrdersForRouteException(route.Id);
        
        //Console.WriteLine("== END GENERATE TIMES ===============");
        
    }


    #endregion
}
