using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
public class StateTrip
{
    #region statement
    private static IMongoCollection<StateTrip> _stateTripColl = MongoDbConnection.GetCollection<StateTrip>("stateTrips");
    #endregion
    
    #region attributes

    private int _id;
    private string _state;

    #endregion

    #region properties

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public string State
    {
        get => _state;
        set => _state = value;
    }

    #endregion

    #region class methods

    public static List<StateTrip> Get()
    {
        List<StateTrip> stateTrips =
        [
            new StateTrip
            {
                Id = 1,
                State = "In transit"
            },

            new StateTrip
            {
                Id = 2,
                State = "Canceled"
            },

            new StateTrip
            {
                Id = 3,
                State = "Completed"
            }
        ];
        return stateTrips;
    }
    
    public static StateTrip Get(int id)
    {
        //Test
        StateTrip state = new StateTrip
        {
            Id = id,
            State = "In transit"
        };
        //End test
        return state;
    }
    #endregion

}
    

