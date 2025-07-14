using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;


public class Truck
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<Truck> _truckColl = MongoDbConnection.GetCollection<Truck>("Trucks");
    
    #endregion

    #region properties

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("brand")]
    public string Brand { get; set; }
    [BsonElement("model")]
    public string Model { get; set; }

    [BsonElement("license_plate")]
    public string LicensePlate { get; set; }

    [BsonElement("IDStateTruck")]
    public string State { get; set; }

    #endregion
    
    #region class methods

    public static List<Truck> Get() 
    {
        return _truckColl.Find(t => true).ToList();
    }
    
    public static List<Truck> GetAvailable() 
    {
        return _truckColl.Find(t => t.State == "AV").ToList();
    }
    
    public static Truck Get(string id)
    {
        return _truckColl.Find(t => t.Id == id).FirstOrDefault();
    }

    public static bool Insert(Truck u)
    {
        try
        {
            _truckColl.InsertOne(u);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
    
    #endregion
}