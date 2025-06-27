using MongoDB.Driver;
using System;

public class Reading
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<Reading> _readingColl = MongoDbConnection.GetCollection<Reading>("readings");
    
    #endregion
    
    #region attributes
    
    private int _id;
    private DateTime _date;
    private bool _doorState;
    private decimal _temp;
    private int _percHumidity;
    private double _latitude;
    private double _longitude;
    private Truck _truck;

    #endregion

    #region properties

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public DateTime Date
    {
        get => _date;
        set => _date = value;
    }

    public bool DoorState
    {
        get => _doorState;
        set => _doorState = value;
    }

    public decimal Temp
    {
        get => _temp;
        set => _temp = value;
    }

    public int PercHumidity
    {
        get => _percHumidity;
        set => _percHumidity = value;
    }

    public double Latitude
    {
        get => _latitude;
        set => _latitude = value;
    }

    public double Longitude
    {
        get => _longitude;
        set => _longitude = value;
    }

    public Truck Truck
    {
        get => _truck;
        set => _truck = value;
    }

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
                Id = 1001,
                Date = DateTime.Now.AddHours(-2),
                DoorState = false,
                Temp = 4.5m,
                PercHumidity = 75,
                Latitude = 32.5123,
                Longitude = -116.7832,
                Truck = Truck.Get(1001)
            },
            new Reading
            {
                Id = 1002,
                Date = DateTime.Now.AddHours(-1),
                DoorState = true,
                Temp = 6.2m,
                PercHumidity = 80,
                Latitude = 32.5145,
                Longitude = -116.7845,
                Truck = Truck.Get(1001)
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
    public static Reading Get(int id)
    {
        //Test
        Reading r = new Reading
        {
            Id = id,
            Date = DateTime.Now.AddHours(-1),
            DoorState = false,
            Temp = 5.1m,
            PercHumidity = 77,
            Latitude = 32.5123,
            Longitude = -116.7832,
            Truck = Truck.Get(1001)
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
            return false;
        }
    }
    
    #endregion
}
