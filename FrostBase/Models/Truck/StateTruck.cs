using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

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
                State = "Funcionando"
            },

            new StateTruck
            {
                Id = 2,
                State = "Fuera de servicio"
            },

            new StateTruck
            {
                Id = 3,
                State = "En reparación"
            }
        ];
        return stateTrucks;
    }
    //Returns the state of a specific truck (test)
    public static StateTruck GetById(int id)
    {
        return Get().FirstOrDefault(s => s.Id == id);
    }

    #endregion
}