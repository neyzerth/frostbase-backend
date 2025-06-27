using MongoDB.Driver;
using System;

public class DoorEvent
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<DoorEvent> _doorEventColl = MongoDbConnection.GetCollection<DoorEvent>("doorEvents");
    
    #endregion
    
    #region attributes
    
    private int _id;
    private bool _state;
    private TimeSpan _timeOpened;
    private Truck _truck;

    #endregion

    #region properties

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public bool State
    {
        get => _state;
        set => _state = value;
    }

    public TimeSpan TimeOpened
    {
        get => _timeOpened;
        set => _timeOpened = value;
    }

    public Truck Truck
    {
        get => _truck;
        set => _truck = value;
    }

    #endregion
    
    #region class methods

    /// <summary>
    /// Returns a list of all door events
    /// </summary>
    /// <returns></returns>
    public static List<DoorEvent> Get() 
    {
        //Test
        List<DoorEvent> doorEvents =
        [
            new DoorEvent
            {
                Id = 1001,
                State = true,
                TimeOpened = new TimeSpan(0, 5, 30),
                Truck = Truck.Get(1001)
            },
            new DoorEvent
            {
                Id = 1002,
                State = false,
                TimeOpened = new TimeSpan(0, 3, 45),
                Truck = Truck.Get(1002)
            },
        ];
        //End test
        
        return doorEvents;
    }

    /// <summary>
    /// Returns the door event with the specified id
    /// </summary>
    /// <param name="id">DoorEvent id</param>
    /// <returns></returns>
    public static DoorEvent Get(int id)
    {
        //Test
        DoorEvent d = new DoorEvent
        {
            Id = id,
            State = true,
            TimeOpened = new TimeSpan(0, 4, 15),
            Truck = Truck.Get(1001)
        };
        //End test
        return d;
    }

    public static bool Insert(DoorEvent d)
    {
        try
        {
            _doorEventColl.InsertOne(d);
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
