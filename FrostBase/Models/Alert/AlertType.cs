using MongoDB.Driver;

namespace FrostBase.Models.Alert;

public class AlertType
{
    #region statement
    private static IMongoCollection<AlertType> _alertTypeColl = MongoDbConnection.GetCollection<AlertType>("alertType");
    #endregion
    
    #region attributes

    private int _id;
    private string _type;
    private string _message;
    
    #endregion

    #region properties

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public string Type
    {
        get => _type;
        set => _type = value;
    }
    
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
                Type = "Puerta abierta",
                Message = "La puerta ha sido abierta por más de 2 minutos."
            },

            new AlertType
            {
                Id = 2,
                Type = "Temperatura alta",
                Message = "La temperatura ha superado los 8°C."
            }
        ];
        return alertTypes;
    }
    //Returns the state of a specific truck (test)
    public static AlertType GetById(int id)
    {
        return Get().FirstOrDefault(s => s.Id == id);
    }

    #endregion
}