using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

public class Route
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<Route> _routeColl = MongoDbConnection.GetCollection<Route>("routes");
    
    #endregion
    
    #region attributes
    
    private int _id;
    private string _name;
    private UserApp _user;
    private List<Store> _stores;

    #endregion

    #region properties

    [BsonId]
    public int Id
    {
        get => _id;
        set => _id = value;
    }

    [BsonElement("name")]
    public string Name
    {
        get => _name;
        set => _name = value;
    }

    [JsonIgnore]
    [BsonElement("IDUser")]
    public int IDUser
    {
        get => _user.Id;
        set => _user.Id = value;
    }

    [BsonIgnore]
    public UserApp User
    {
        get => _user;
        set => _user = value;
    }

    [BsonElement("stores")]
    public List<Store> Stores
    {
        get => _stores;
        set => _stores = value;
    }

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
                User = UserApp.Get(1001),
                Stores = Store.Get()
            },
            new Route
            {
                Id = 1002,
                Name = "Eastside Route",
                User = UserApp.Get(1002),
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
            User = UserApp.Get(1001),
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
    
    #endregion
}
