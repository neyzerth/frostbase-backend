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

    public static Parameter Get() 
    { 
        return _parameterColl.Find(p => true).FirstOrDefault();
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
            Console.WriteLine("Parameter insert: "+e);
            throw new Exception("Error inserting parameter: "+e.Message);
        }
    }
    
    public static Parameter Insert(CreateParameterDto p)
    {
        Parameter alert = new Parameter
        {
            MaxTemperature = p.MaxTemperature,
            MinTemperature = p.MinTemperature,
            MaxHumidity = p.MaxHumidity,
            MinHumidity = p.MinHumidity
        };

        return Insert(alert);
    }
    
    public static Parameter Update(Parameter updatedParameter)
    {
        try
        {
            var filter = Builders<Parameter>.Filter.Eq(p => p.Id, 1);
            var update = Builders<Parameter>.Update
                .Set(p => p.MaxTemperature, updatedParameter.MaxTemperature)
                .Set(p => p.MinTemperature, updatedParameter.MinTemperature)
                .Set(p => p.MaxHumidity, updatedParameter.MaxHumidity)
                .Set(p => p.MinHumidity, updatedParameter.MinHumidity);
            
            if (updatedParameter.MinHumidity < 0 || updatedParameter.MinHumidity > 100 ||
                updatedParameter.MaxHumidity < 0 || updatedParameter.MaxHumidity > 100)
            {
                throw new ArgumentException("Humidity values must be between 0 and 100.");
            }
            
            var options = new FindOneAndUpdateOptions<Parameter>
            {
                ReturnDocument = ReturnDocument.After // Retorna el documento ya actualizado
            };

            return _parameterColl.FindOneAndUpdate(filter, update, options);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error updating parameters: " + e.Message);
        }
    }

    public static Parameter Update(UpdateParameterDto updatedParameter)
    {
        var param = new Parameter
        {
            MaxTemperature = updatedParameter.MaxTemperature,
            MinTemperature = updatedParameter.MinTemperature,
            MaxHumidity = updatedParameter.MaxHumidity,
            MinHumidity = updatedParameter.MinHumidity
            
        };
        return Update(param);
    }
}
    
    #endregion

