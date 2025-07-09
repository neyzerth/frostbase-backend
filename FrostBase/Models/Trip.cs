using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

public class Trip
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<Trip> _tripColl = 
        MongoDbConnection.GetCollection<Trip>("Trips");
    
    #endregion

    #region properties

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("start_time")]
    public DateTime StartTime { get; set; }

    [BsonElement("end_time")]   
    public DateTime? EndTime { get; set; }

    [BsonElement("total_time")] 
    public TimeSpan? TotalTime { get; set; }
    
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
    
    #region class methods

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
            
            
            _tripColl.InsertOne(t);
            return t;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
    public static Trip Insert(CreateTripDto c)
    {
        Trip t = new Trip
        {
            Id = c.Id, 
            StartTime = c.StartTime,
            EndTime = c.EndTime,
            IDTruck = c.IdTruck,
            IDUser = c.IdDriver,
            IDRoute = c.IdRoute,
            IDStateTrip = c.State,
            
        };
        
        return Insert(t);
    }
    public static Trip Insert(StartTripDto c)
    {
        Trip t = new Trip
        {
            StartTime = c.StartHour,
            IDRoute = c.IDRoute,
            IDStateTrip = c.State,
            Orders = new List<TripOrder>()
        };
        
        t.Orders.Add(new TripOrder
        {
            IDOrder = ObjectId.GenerateNewId().ToString(),
            IDStore = ObjectId.GenerateNewId().ToString(),
            StartTime = DateTime.Now,
            EndTime = null
        });
        return Insert(t);
    }
    
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
                .Set(t => t.IDStateTrip, "compl")
                .Set(t => t.TotalTime, totalTime);
                
            return _tripColl.FindOneAndUpdate(filter, update,
                    new FindOneAndUpdateOptions<Trip>
                    { ReturnDocument = ReturnDocument.After }
                );
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
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

            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
    
    public static Trip EndOrder(string tripId, string orderId)
    {
        try
        {
            Trip trip = _tripColl.Find(t => t.Id == tripId).FirstOrDefault();
            TimeSpan totalTime = DateTime.Now.TimeOfDay.Subtract(trip.StartTime.TimeOfDay);
            
            if (trip == null)
                return null; // El viaje no existe
            
            if (trip.Orders == null || trip.Orders.All(o => o.IDOrder != orderId))
                return null; // La orden específica no existe
            
            var filter = Builders<Trip>.Filter.And(
                Builders<Trip>.Filter.Eq(t => t.Id, trip.Id),
                Builders<Trip>.Filter.ElemMatch(t => t.Orders, o => o.IDOrder == orderId)
            );
            
            var update = Builders<Trip>.Update
                .Set(t => t.TotalTime, totalTime)
                .Set(t => t.Orders[0].EndTime, DateTime.Now);
                
            var result = _tripColl.FindOneAndUpdate(filter, update,
                new FindOneAndUpdateOptions<Trip>
                    { ReturnDocument = ReturnDocument.After }
                );
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
    

    #endregion
}
