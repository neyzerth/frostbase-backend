using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Alert
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<Alert> _alertColl = MongoDbConnection.GetCollection<Alert>("Alerts");
    
    #endregion
    
    #region properties
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id
    { get; set; }
    
    [BsonElement("state")]
    public bool State { get; set; }
    
    [BsonElement("date")] 
    public DateTime Date { get; set; }
    
    [BsonElement("IDAlertTypes")] 
    public string AlertType { get; set; }

    [BsonElement("IDReading")] 
    [BsonRepresentation(BsonType.ObjectId)]
    public string IDReading { get; set; }

    #endregion
    
    #region class methods

    public static List<Alert> Get() 
    {
        return _alertColl.Find(a => true).ToList();
    }

    public static Alert Get(string id)
    {
        return _alertColl.Find(a => a.Id == id).FirstOrDefault();
    }

    public static bool Insert(Alert a)
    {
        try
        {
            _alertColl.InsertOne(a);
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
