using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

public class AlertType
{
    #region statement
    private static IMongoCollection<AlertType> _alertTypeColl = MongoDbConnection.GetCollection<AlertType>("AlertType");
    #endregion
    

    #region properties

    [BsonId]
    public string Id { get; set; }
    
    [BsonElement("type")]
    public string Type { get; set; }
    
    [BsonElement("message")]
    public string Message { get; set; }
    
    #endregion
    
    #region class methods
    //Returns a list of 
    public static List<AlertType> Get()
    {
        return _alertTypeColl.Find(_ => true).ToList();
    }
    //Returns the alert of a specific truck
    public static AlertType GetById(string id)
    {
        return _alertTypeColl.Find(a => a.Id == id).FirstOrDefault();
    }

    #endregion
}