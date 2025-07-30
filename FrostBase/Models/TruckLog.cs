using System.Runtime.InteropServices.JavaScript;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

public class TruckLog
{
    private static IMongoCollection<TruckLog> _truckLogColl = 
        MongoDbConnection.GetCollection<TruckLog>("TruckLogs");
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonElement("date")]
    public DateTime Date { get; set; }
    
    [BsonElement("IDTruck")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IDTruck { get; set; }
    
    [BsonElement("IDStateTruck")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IDStateTruck { get; set; }
    
    public static List<TruckLog> Get()
    {
        return _truckLogColl.Find(t => true).ToList();      
    }
    public static List<TruckLog> GetByDate(DateTime date)
    {
        return _truckLogColl.Find(t => t.Date == date).ToList();      
    }
    public static List<TruckLog> GetByTruck(string truckId)
    {
        return _truckLogColl.Find(t => t.IDTruck == truckId).ToList();      
    }

    public static List<TruckLog> GetAvalaible(DateTime date)
    {
        return _truckLogColl.Find(t => t.IDStateTruck == "AV" && t.Date <= date).ToList();
    }

    public static TruckLog Insert(TruckLog t)
    {
        try
        {
            if(string.IsNullOrEmpty(t.Id))
                t.Id = ObjectId.GenerateNewId().ToString();
            _truckLogColl.InsertOne(t);
            return t;
        }
        catch (Exception e)
        {
            Console.WriteLine("Truck log insert: "+e);
            throw new Exception("Error inserting truck log: "+e.Message);
        }
    }

    public static TruckLog Insert(Truck truck, DateTime? date)
    {
        DateTime newDate = date ?? DateTime.Now;
        var newTruck = new TruckLog
        {
            IDTruck = truck.Id,
            IDStateTruck = truck.IDStateTruck,
            Date = newDate
        };
        
        return Insert(newTruck);

    }
}