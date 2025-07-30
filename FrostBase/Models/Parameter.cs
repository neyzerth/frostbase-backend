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
        if (string.IsNullOrEmpty(updatedParameter.Id))
            throw new ArgumentException("Id cannot be null or empty");

        try
        {
            var filter = Builders<Order>.Filter.Eq(p => p.Id, updatedParameter.Id);
            var update = Builders<Order>.Update
                .Set(p => p.MaxTemperature, updatedParameter.MaxTemperature)
                .Set(p => p.MinTemperature, updatedParameter.MinTemperature)
                .Set(p => p.MaxHumidity, updatedParameter.MaxHumidity)
                .Set(p => p.MaxHumidity, updatedParameter.MinHumidity);
            
            var options = new FindOneAndUpdateOptions<Parameter>
            {
                ReturnDocument = ReturnDocument.After // Retorna el documento ya actualizado
            };

            return _parameterColl.FindOneAndUpdate(filter, update, options);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error updating order: " + e.Message);
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
}
