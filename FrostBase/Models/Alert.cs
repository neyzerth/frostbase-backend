using MongoDB.Driver;
using System;
using FrostBase.Models.Alert;
using MongoDB.Bson.Serialization.Attributes;

public class Alert
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<Alert> _alertColl = MongoDbConnection.GetCollection<Alert>("alerts");
    
    #endregion
    
    #region attributes
    
    private int _id;
    private bool _state;
    private DateTime _date;
    private decimal _detectedValue;
    private AlertType _alertType;
    private Truck _truck;

    #endregion

    #region properties
    
    [BsonId]
    public int Id
    {
        get => _id;
        set => _id = value;
    }
    
    [BsonElement("state")]
    public bool State
    {
        get => _state;
        set => _state = value;
    }
    
    [BsonElement("dateTime")] 
    public DateTime Date
    {
        get => _date;
        set => _date = value;
    }
    
    [BsonElement("detectedValue")] 
    public decimal DetectedValue
    {
        get => _detectedValue;
        set => _detectedValue = value;
    }
    
    [BsonElement("alertType")] 
    public AlertType AlertType
    {
        get => _alertType;
        set => _alertType = value;
    }

    [BsonElement("truck")] 
    public Truck Truck
    {
        get => _truck;
        set => _truck = value;
    }

    #endregion
    
    #region class methods

    /// <summary>
    /// Returns a list of all alerts
    /// </summary>
    /// <returns></returns>
    public static List<Alert> Get() 
    {
        //Test
        List<Alert> alerts =
        [
            new Alert
            {
                Id = 1001,
                Date = DateTime.Now.AddHours(-2),
                State = true,
                DetectedValue = 9.5m,
                AlertType = AlertType.GetById(1001),
                Truck = Truck.Get(1001)
            },
            new Alert
            {
                Id = 1002,
                State = true,
                Date = DateTime.Now.AddHours(-1),
                DetectedValue = 0.0m,
                AlertType = AlertType.GetById(1001),
                Truck = Truck.Get(1002)
            },
        ];
        //End test
        
        return alerts;
    }

    /// <summary>
    /// Returns the alert with the specified id
    /// </summary>
    /// <param name="id">Alert id</param>
    /// <returns></returns>
    public static Alert Get(int id)
    {
        //Test
        Alert a = new Alert
        {
            Id = id,
            Date = DateTime.Now.AddHours(-3),
            State = true,
            DetectedValue = 1.5m,
            AlertType = AlertType.GetById(1001),
            Truck = Truck.Get(1001)
        };
        //End test
        return a;
    }

    public static bool Insert(Alert a)
    {
        try
        {
            _alertColl.InsertOne(a);
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
