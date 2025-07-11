using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

public class Route
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<Route> _routeColl = MongoDbConnection.GetCollection<Route>("Routes");
    
    #endregion
    
    #region properties

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }
    
    [BsonElement("deliverDays")]
    public List<int> DeliverDays{ get; set; }
    
    [BsonElement("IDAssignedUser")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IDUser { get; set; }
    
    [BsonElement("active")]
    public bool Active { get; set; }
    
    [BsonElement("stores")]
    public List<RouteStore> Stores { get; set; }

    #endregion
    
    #region class methods

    public static List<Route> Get() 
    {
        return _routeColl.Find(r => r.Active == true).ToList();
    }
    
    public static Route Get(string id)
    {
        return _routeColl.Find(r => r.Id == id).FirstOrDefault();
    }

    public static bool Insert(Route r)
    {
        try
        {
            _routeColl.InsertOne(r);
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
