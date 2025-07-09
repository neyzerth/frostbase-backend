using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

public class State
{
    [BsonId]
    public string Id { get; set; }
    [BsonElement("message")]
    public string Message { get; set; }
}

public class StateTrip : State
{
    private static IMongoCollection<StateTrip> _stateTripColl = MongoDbConnection.GetCollection<StateTrip>("StateTrip");

    #region class methods
    
    public static List<StateTrip> Get()
    { return _stateTripColl.Find(_ => true).ToList(); }
    
    public static StateTrip Get(string id)
    { return _stateTripColl.Find(st => st.Id == id).FirstOrDefault(); }
    #endregion

}
public class StateOrder : State
{
    private static IMongoCollection<StateOrder> _stateOrderColl = MongoDbConnection.GetCollection<StateOrder>("StateOrder");

    #region class methods
    
    public static List<StateOrder> Get()
    { return _stateOrderColl.Find(_ => true).ToList(); }
    
    public static StateOrder Get(string id)
    { return _stateOrderColl.Find(st => st.Id == id).FirstOrDefault(); }
    #endregion

}
public class StateTruck : State
{
    private static IMongoCollection<StateTruck> _stateTruckColl = MongoDbConnection.GetCollection<StateTruck>("StateTruck");

    #region class methods
    
    public static List<StateTruck> Get()
    { return _stateTruckColl.Find(_ => true).ToList(); }
    
    public static StateTruck Get(string id)
    { return _stateTruckColl.Find(st => st.Id == id).FirstOrDefault(); }
    #endregion

}
public class AlertType : State
{
    private static IMongoCollection<AlertType> _alertType = MongoDbConnection.GetCollection<AlertType>("AlertTypes");

    #region class methods
    
    public static List<AlertType> Get()
    { return _alertType.Find(_ => true).ToList(); }
    
    public static AlertType Get(string id)
    { return _alertType.Find(st => st.Id == id).FirstOrDefault(); }
    #endregion

}