using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace FrostBase.Models.Alert;

public class AlertType
{
    #region statement
    private static IMongoCollection<AlertType> _alertTypeColl = MongoDbConnection.GetCollection<AlertType>("alertTypes");
    #endregion
    
    #region attributes

    private int _id;
    private string _type;
    private string _message;
    
    #endregion

    #region properties

    [BsonId]
    public int Id
    {
        get => _id;
        set => _id = value;
    }
    
    [BsonElement("type")]
    public string Type
    {
        get => _type;
        set => _type = value;
    }
    
    [BsonElement("message")]
    public string Message
    {
        get => _message;
        set => _message = value;
    }

    #endregion
    
    #region class methods
    //Returns a list of 
    public static List<AlertType> Get()
    {
        List<AlertType> alertTypes =
        [
            new AlertType
            {
                Id = 1,
                Type = "Open door",
                Message = "Door has been opened for up to 2 minutes"
            },

            new AlertType
            {
                Id = 2,
                Type = "High temperature",
                Message = "Temperature has increased to 8°C."
            }
        ];
        return alertTypes;
    }
    //Returns the alert of a specific truck
    public static AlertType GetById(int id)
    {
        AlertType alert = new AlertType
        {
            Id = id,
            Type = "Open door",
            Message = "Door has been opened for up to 2 minutes"
        };
        
        return alert;
    }

    #endregion
}