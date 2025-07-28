using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

public class TripLog
{
    public static IMongoCollection<TripLog> _tripLogColl = 
        MongoDbConnection.GetCollection<TripLog>("TripLogs");
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonElement("date")]
    public DateTime Date { get; set; }    
    
    [BsonElement("IDTrip")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IDTrip { get; set; }
    
    [BsonElement("IDStateTrip")]
    public string IDStateTrip { get; set; }
    
    [BsonElement("orders")]
    public List<TripOrder> Orders { get; set; }

    public static List<TripLog> Get()
    {
        return _tripLogColl.Find(t => true).ToList();      
    }
    public static List<TripLog> GetByDate(DateTime date)
    {
        return _tripLogColl.Find(t => t.Date == date).ToList();      
    }
    public static List<TripLog> GetByTrip(string tripId)
    {
        return _tripLogColl.Find(t => t.Id == tripId).ToList();      
    }
    public static TripLog Get(string id)
    {
        return _tripLogColl.Find(t => t.Id == id).FirstOrDefault();      
    }

    public TripLog()
    {
        Id = ObjectId.GenerateNewId().ToString();
        Date = DateTime.Now;
        Orders = new List<TripOrder>();
        IDTrip = "";
        IDStateTrip = "";
    }

    public TripLog(Trip t, DateTime? date)
    {
        Id = ObjectId.GenerateNewId().ToString();
        Date = date?? DateTime.Now;
        Orders = t.Orders;
        IDTrip = string.IsNullOrEmpty(t.Id)? ObjectId.GenerateNewId().ToString() : t.Id;
        IDStateTrip = t.IDStateTrip;
    }

    public static TripLog Insert(TripLog t)
    {
        try
        {
            if(string.IsNullOrEmpty(t.Id))
                t.Id = ObjectId.GenerateNewId().ToString();
            _tripLogColl.InsertOne(t);
            return t;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error inserting trip log: "+e.Message);
        }
    }

    public static TripLog Insert(Trip t, DateTime? date)
    {
        try
        {
            if(string.IsNullOrEmpty(t.Id))
                t.Id = ObjectId.GenerateNewId().ToString();
            var log = new TripLog
            {
                Date = date?? DateTime.Now,
                IDTrip = t.Id,
                IDStateTrip = t.IDStateTrip,
                Orders = t.Orders
            };
            
            return Insert(log);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error inserting log of trip "+ t.Id+": "+e.Message);
        }
    }
}