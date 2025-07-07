using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

public class Reading
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<Reading> _readingColl = MongoDbConnection.GetCollection<Reading>("readings");
    
    #endregion
    
    #region attributes
    
    private string _id;
    private DateTime _date;
    private bool _doorState;
    private double _temp;
    private int _percHumidity;
    private double _latitude;
    private double _longitude;
    private string _idTruck;

    #endregion

    #region properties

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id
    {
        get => _id;
        set => _id = value;
    }

    [BsonElement("date")]
    public DateTime Date
    {
        get => _date;
        set => _date = value;
    }

    [BsonElement("door_state")]
    public bool DoorState
    {
        get => _doorState;
        set => _doorState = value;
    }

    [BsonElement("temp")]
    public double Temperature
    {
        get => _temp;
        set => _temp = value;
    }

    [BsonElement("perc_humidity")]
    public int PercHumidity
    {
        get => _percHumidity;
        set => _percHumidity = value;
    }

    [BsonElement("latitude")]
    public double Latitude
    {
        get => _latitude;
        set => _latitude = value;
    }

    [BsonElement("longitude")]
    public double Longitude
    {
        get => _longitude;
        set => _longitude = value;
    }

    [BsonElement("IDTruck")]
    public string IDTruck { get; set; }

    #endregion
    
    #region class methods

    /// <summary>
    /// Returns a list of all readings
    /// </summary>
    /// <returns></returns>
    public static List<Reading> Get() 
    {
        //Test
        List<Reading> readings =
        [
            new Reading
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Date = DateTime.Now.AddHours(-2),
                DoorState = false,
                Temperature = 4.5,
                PercHumidity = 75,
                Latitude = 32.5123,
                Longitude = -116.7832,
                IDTruck = ObjectId.GenerateNewId().ToString(),
            },
            new Reading
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Date = DateTime.Now.AddHours(-1),
                DoorState = true,
                Temperature = 6.2,
                PercHumidity = 80,
                Latitude = 32.5145,
                Longitude = -116.7845,
                IDTruck =ObjectId.GenerateNewId().ToString(),
            },
        ];
        //End test
        
        return readings;
    }

    /// <summary>
    /// Returns the reading with the specified id
    /// </summary>
    /// <param name="id">Reading id</param>
    /// <returns></returns>
    public static Reading Get(string id)
    {
        //Test
        Reading r = new Reading
        {
            Id = id,
            Date = DateTime.Now.AddHours(-1),
            DoorState = false,
            Temperature = 5.1,
            PercHumidity = 77,
            Latitude = 32.5123,
            Longitude = -116.7832,
            IDTruck = ObjectId.GenerateNewId().ToString(),
        };
        //End test
        return r;
    }

    public static bool Insert(Reading r)
    {
        try
        {
            _readingColl.InsertOne(r);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static bool Insert(string idTruck, CreateReadingDto c)
    {
        Reading r = new Reading
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Date = DateTime.Now,
            IDTruck = idTruck,
            DoorState = c.DoorState,
            Latitude = c.Latitude,
            Longitude = c.Longitude,
            PercHumidity = c.Humidity,
            Temperature = c.Temperature,
        };
        
        return Insert(r);
    }
    
    #endregion
}
