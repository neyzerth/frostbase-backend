using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
public class StateTrip
{
    #region statement
    private static IMongoCollection<StateTrip> _stateTripColl = MongoDbConnection.GetCollection<StateTrip>("StateTrip");
    #endregion
    
    #region properties
    
    [BsonId]
    public string Id { get; set; }
    
    [BsonElement("state")]
    public string State { get; set; }
    
    #endregion
    
    #region class methods
    
    public static List<StateTrip> Get()
    {
        return _stateTripColl.Find(_ => true).ToList();
    }
    
    public static StateTrip Get(string id)
    {
        return _stateTripColl.Find(st => st.Id == id).FirstOrDefault();
    }
    #endregion

}
    

