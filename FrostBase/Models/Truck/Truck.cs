using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

public class Truck
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<Truck> _truckColl = MongoDbConnection.GetCollection<Truck>("trucks");
    
    #endregion
    
    #region attributes
    
    private int _id;
    private string _brand;
    private string _model;
    private string _licensePlate;
    private int _capacity;
    private bool _state;


    #endregion

    #region properties

    [BsonId]
    public int Id
    {
        get => _id;
        set => _id = value;
    }
    [BsonElement("brand")]
    public string Brand
    {
        get => _brand;
        set => _brand = value;
    }
    [BsonElement("model")]
    public string Model
    {
        get => _model;
        set => _model = value;
    }

    [BsonElement("licensePlate")]
    public string LicensePlate
    {
        get => _licensePlate;
        set => _licensePlate = value;
    }

    [BsonElement("capacity")]
    public int Capacity
    {
        get => _capacity;
        set => _capacity = value;
    }

    [BsonElement("state")]
    public bool State
    {
        get => _state;
        set => _state = value;
    }

    #endregion
    
    #region class methods

    /// <summary>
    /// Returns a list of all trucks
    /// </summary>
    /// <returns></returns>
    public static List<Truck> Get() 
    {
        //Test
        List<Truck> trucks =
        [
            new Truck
            {
                Id = 1001,
                Brand = "Test",
                Model = "Test",
                LicensePlate = "836DAS92",
                Capacity = 100,
                State = true
            },
            new Truck
            {
                Id = 1002,
                Brand = "Test",
                Model = "Test",
                LicensePlate = "AS92JAK3",
                Capacity = 100,
                State = true
            },
        ];
        //End test
        
        return trucks;
    }

    /// <summary>
    /// Returns the user with the specified id
    /// </summary>
    /// <param name="id">Truck id</param>
    /// <returns></returns>
    public static Truck Get(int id)
    {
        //Test
        
        Truck u = new Truck
        {
            Id = id,
            Brand = "Test",
            Model = "Test",
            LicensePlate = "JAKAS3R3",
            Capacity = 100,
            State = true
        };
        //End test
        return u;
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