using MongoDB.Driver;
using System;
using FrostBase.Enumerators;

public class Alert
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<Alert> _alertColl = MongoDbConnection.GetCollection<Alert>("alerts");
    
    #endregion
    
    #region attributes
    
    private int _id;
    private string _message;
    private DateTime _date;
    private bool _state;
    private decimal _detectedValue;
    private AlertType _alertType;
    private Truck _truck;

    #endregion

    #region properties

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public string Message
    {
        get => _message;
        set => _message = value;
    }

    public DateTime Date
    {
        get => _date;
        set => _date = value;
    }

    public bool State
    {
        get => _state;
        set => _state = value;
    }

    public decimal DetectedValue
    {
        get => _detectedValue;
        set => _detectedValue = value;
    }

    public AlertType AlertType
    {
        get => _alertType;
        set => _alertType = value;
    }

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
                Message = "Temperature too high",
                Date = DateTime.Now.AddHours(-2),
                State = true,
                DetectedValue = 9.5m,
                AlertType = AlertType.HighTemperature,
                Truck = Truck.Get(1001)
            },
            new Alert
            {
                Id = 1002,
                Message = "Door left open",
                Date = DateTime.Now.AddHours(-1),
                State = false,
                DetectedValue = 0.0m,
                AlertType = AlertType.DoorOpen,
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
            Message = "Temperature too low",
            Date = DateTime.Now.AddHours(-3),
            State = true,
            DetectedValue = 1.5m,
            AlertType = AlertType.LowTemperature,
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
