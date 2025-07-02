using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
namespace FrostBase.Models.Truck;

public class StateTruck
{
    #region statement
    private static IMongoCollection<StateTrip> _stateTruckColl = MongoDbConnection.GetCollection<StateTrip>("stateTruck");
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
    //Returns a list of the statesTruck
    public static List<StateTruck> Get()
    {
        List<StateTruck> stateTrucks =
        [
            new StateTruck
            {
                Id = 1,
                State = "Operating"
            },

            new StateTruck
            {
                Id = 2,
                State = "Out of service"
            },

            new StateTruck
            {
                Id = 3,
                State = "In maintenance"
            },
            
            new StateTruck
            {
            Id = 4,
            State = "Under repair"
            }
        ];
        return stateTrucks;
    }
    //Returns the state of a specific truck (test)
    public static StateTruck Get(int id)
    {
        StateTruck state = new StateTruck
        {
            Id = id,
            State = "Operating"

        };
        //End test
        return state;
    }

    #endregion
}