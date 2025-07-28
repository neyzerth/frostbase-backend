using MongoDB.Driver;
using System;
using MongoDB.Bson.Serialization.Attributes;

public class Parameter
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<Parameter> _parameterColl = MongoDbConnection.GetCollection<Parameter>("Parameters");
    
    #endregion
    
    #region properties

    [BsonId]
    public int Id { get; set; }

    [BsonElement("max_temperature")]
    public decimal MaxTemperature{ get; set; }
    [BsonElement("max_humidity")]
    public int MaxHumidity{ get; set; }

    [BsonElement("min_temperature")]
    public decimal MinTemperature{ get; set; }

    [BsonElement("min_humidity")]
    public int MinHumidity{ get; set; }

    #endregion
    
    #region class methods

    public static List<Parameter> Get() 
    {
        return _parameterColl.Find(p => true).ToList();
    }

    public static Parameter Get(int id)
    {
        return _parameterColl.Find(p => p.Id == id).FirstOrDefault();       
    }

    public static Parameter Insert(Parameter p)
    {
        try
        {
            _parameterColl.InsertOne(p);
            return p;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error inserting parameter: "+e.Message);
        }
    }
    
    #endregion
}
