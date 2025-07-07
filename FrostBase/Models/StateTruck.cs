using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

public class StateTruck
{
    #region statement
    private static IMongoCollection<StateTruck> _stateTruckColl = MongoDbConnection.GetCollection<StateTruck>("StateTruck");
    #endregion
    
    #region properties
    
    [BsonId]
    public string Id { get; set; }
    
    [BsonElement("state")]
    public string State { get; set; }
    
    #endregion
    
    #region class methods
    //Returns a list of the statesTruck
    public static List<StateTruck> Get()
    {
        return _stateTruckColl.Find(_ => true).ToList();
    }
    //Returns the state of a specific truck
    public static StateTruck Get(string id)
    {
        return _stateTruckColl.Find(st => st.Id == id).FirstOrDefault();
    }

    #endregion
}