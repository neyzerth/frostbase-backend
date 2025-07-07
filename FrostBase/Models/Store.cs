using MongoDB.Driver;
using System;
using MongoDB.Bson.Serialization.Attributes;

public class Store
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<Store> _storeColl = MongoDbConnection.GetCollection<Store>("Stores");
    
    #endregion
    

    #region properties

    [BsonId]
    public int Id { get; set; }
    
    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("phone")]
    public string Phone { get; set; }

    [BsonElement("location")]
    public string Location { get; set; }

    [BsonElement("latitude")]
    public double Latitude { get; set; }

    [BsonElement("longitude")]
    public double Longitude { get; set; }

    #endregion
    
    #region class methods

    public static List<Store> Get() 
    {
        return _storeColl.Find(s => true).ToList();       
    }

    /// <summary>
    /// Returns the store with the specified id
    /// </summary>
    /// <param name="id">Store id</param>
    /// <returns></returns>
    public static Store Get(int id)
    {
        return _storeColl.Find(s => s.Id == id).FirstOrDefault();       
    }

    public static bool Insert(Store s)
    {
        try
        {
            _storeColl.InsertOne(s);
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
