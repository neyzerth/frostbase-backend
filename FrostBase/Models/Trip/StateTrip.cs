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
                State = "En ruta"
            },

            new StateTrip
            {
                Id = 2,
                State = "Cancelado"
            },

            new StateTrip
            {
                Id = 3,
                State = "Completado"
            }
        ];
        return stateTrips;
    }
    
    

    #endregion

}
    

