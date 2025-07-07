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
    public int Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }
    
    [JsonIgnore]
    [BsonElement("IDUser")]
    public int IDUser { get; set; }
    
    [BsonElement("stores")]
    public List<Store> Stores { get; set; }

    #endregion
    
    #region class methods

    public static List<Route> Get() 
    {
        return _routeColl.Find(r => true).ToList();
    }
    
    public static Route Get(int id)
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
