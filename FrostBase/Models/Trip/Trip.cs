using MongoDB.Driver;
using System;
using System.Collections.Generic;

public class Trip
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<Trip> _tripColl = MongoDbConnection.GetCollection<Trip>("trips");
    
    #endregion
    
    #region attributes
    
    private int _id;
    private DateTime _date;
    private TimeSpan _startHour;
    private TimeSpan _endHour;
    private bool _state;
    private TimeSpan _totalTime;
    private int _idRoute;
    private List<Order> _orders;

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

    public TimeSpan StartHour
    {
        get => _startHour;
        set => _startHour = value;
    }

    public TimeSpan EndHour
    {
        get => _endHour;
        set => _endHour = value;
    }

    public bool State
    {
        get => _state;
        set => _state = value;
    }

    public TimeSpan TotalTime
    {
        get => _totalTime;
        set => _totalTime = value;
    }

    public int IDRoute
    {
        get => _idRoute;
        set => _idRoute = value;
    }

    public List<Order> Orders
    {
        get => _orders;
        set => _orders = value;
    }

    #endregion
    
    #region class methods

    /// <summary>
    /// Returns a list of all trips
    /// </summary>
    /// <returns></returns>
    public static List<Trip> Get() 
    {
        //Test
        List<Trip> trips =
        [
            new Trip
            {
                Id = 1001,
                Date = DateTime.Now,
                StartHour = new TimeSpan(8, 0, 0),
                EndHour = new TimeSpan(12, 0, 0),
                State = true,
                TotalTime = new TimeSpan(4, 0, 0),
                IDRoute = 1,
                Orders = new List<Order>()
            },
            new Trip
            {
                Id = 1002,
                Date = DateTime.Now.AddDays(1),
                StartHour = new TimeSpan(9, 0, 0),
                EndHour = new TimeSpan(14, 0, 0),
                State = true,
                TotalTime = new TimeSpan(5, 0, 0),
                IDRoute = 2,
                Orders = new List<Order>()
            },
        ];
        //End test
        
        return trips;
    }

    /// <summary>
    /// Returns the trip with the specified id
    /// </summary>
    /// <param name="id">Trip id</param>
    /// <returns></returns>
    public static Trip Get(int id)
    {
        //Test
        Trip t = new Trip
        {
            Id = id,
            Date = DateTime.Now,
            StartHour = new TimeSpan(8, 0, 0),
            EndHour = new TimeSpan(12, 0, 0),
            State = true,
            TotalTime = new TimeSpan(4, 0, 0),
            IDRoute = 1,
            Orders = new List<Order>()
        };
        //End test
        return t;
    }

    public static bool Insert(Trip t)
    {
        try
        {
            _tripColl.InsertOne(t);
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
