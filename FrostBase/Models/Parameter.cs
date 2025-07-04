using MongoDB.Driver;
using System;
using MongoDB.Bson.Serialization.Attributes;

public class Parameter
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<Parameter> _parameterColl = MongoDbConnection.GetCollection<Parameter>("parameters");
    
    #endregion
    
    #region attributes
    
    private int _id;
    private decimal _maxTemperature;
    private int _maxHumidity;
    private decimal _minTemperature;
    private int _minHumidity;

    #endregion

    #region properties

    [BsonId]
    public int Id
    {
        get => _id;
        set => _id = value;
    }

    [BsonElement("max_temperature")]
    public decimal MaxTemperature
    {
        get => _maxTemperature;
        set => _maxTemperature = value;
    }
    [BsonElement("max_humidity")]
    public int MaxHumidity
    {
        get => _maxHumidity;
        set => _maxHumidity = value;
    }

    [BsonElement("min_temperature")]
    public decimal MinTemperature
    {
        get => _minTemperature;
        set => _minTemperature = value;
    }

    [BsonElement("min_humidity")]
    public int MinHumidity
    {
        get => _minHumidity;
        set => _minHumidity = value;
    }

    #endregion
    
    #region class methods

    /// <summary>
    /// Returns a list of all parameters
    /// </summary>
    /// <returns></returns>
    public static List<Parameter> Get() 
    {
        //Test
        List<Parameter> parameters =
        [
            new Parameter
            {
                Id = 1001,
                MaxTemperature = 8.5m,
                MaxHumidity = 90,
                MinTemperature = 2.0m,
                MinHumidity = 65
            },
            new Parameter
            {
                Id = 1002,
                MaxTemperature = 5.0m,
                MaxHumidity = 85,
                MinTemperature = 0.0m,
                MinHumidity = 70
            },
        ];
        //End test
        
        return parameters;
    }

    /// <summary>
    /// Returns the parameter with the specified id
    /// </summary>
    /// <param name="id">Parameter id</param>
    /// <returns></returns>
    public static Parameter Get(int id)
    {
        //Test
        Parameter p = new Parameter
        {
            Id = id,
            MaxTemperature = 8.0m,
            MaxHumidity = 85,
            MinTemperature = 2.0m,
            MinHumidity = 70
        };
        //End test
        return p;
    }

    public static bool Insert(Parameter p)
    {
        try
        {
            _parameterColl.InsertOne(p);
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
