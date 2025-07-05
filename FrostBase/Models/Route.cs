using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

public class Route
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<Route> _routeColl = MongoDbConnection.GetCollection<Route>("routes");
    
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

    /// <summary>
    /// Returns a list of all routes
    /// </summary>
    /// <returns></returns>
    public static List<Route> Get() 
    {
        //Test
        List<Route> routes =
        [
            new Route
            {
                Id = 1001,
                Name = "Downtown Route",
                IDUser = 1001,
                Stores = Store.Get()
            },
            new Route
            {
                Id = 1002,
                Name = "Eastside Route",
                IDUser = 1002,
                Stores = Store.Get()
            },
        ];
        //End test
        
        return routes;
    }

    /// <summary>
    /// Returns the route with the specified id
    /// </summary>
    /// <param name="id">Route id</param>
    /// <returns></returns>
    public static Route Get(int id)
    {
        //Test
        Route r = new Route
        {
            Id = id,
            Name = "Test Route",
            IDUser = 1001,
            Stores = Store.Get()
        };
        //End test
        return r;
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

    public static Route GetByUser(int idUser)
    {
        //test
        return Get(1001);
        
        //return _routeColl.Find(r => r.IDUser == idUser).FirstOrDefault();
    }
    
    #endregion
}
