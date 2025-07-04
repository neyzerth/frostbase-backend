using MongoDB.Driver;
using System;
using MongoDB.Bson.Serialization.Attributes;

public class Store
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<Store> _storeColl = MongoDbConnection.GetCollection<Store>("stores");
    
    #endregion
    
    #region attributes
    
    private int _id;
    private string _name;
    private string _phone;
    private string _location;
    private double _latitude;
    private double _longitude;

    #endregion

    #region properties

    [BsonId]
    public int Id
    {
        get => _id;
        set => _id = value;
    }
    
    [BsonElement("name")]
    public string Name
    {
        get => _name;
        set => _name = value;
    }

    [BsonElement("phone")]
    public string Phone
    {
        get => _phone;
        set => _phone = value;
    }

    [BsonElement("location")]
    public string Location
    {
        get => _location;
        set => _location = value;
    }

    [BsonElement("latitude")]
    public double Latitude
    {
        get => _latitude;
        set => _latitude = value;
    }

    [BsonElement("longitude")]
    public double Longitude
    {
        get => _longitude;
        set => _longitude = value;
    }

    #endregion
    
    #region class methods

    /// <summary>
    /// Returns a list of all stores
    /// </summary>
    /// <returns></returns>
    public static List<Store> Get() 
    {
        //Test
        List<Store> stores =
        [
            new Store
            {
                Id = 1001,
                Name = "Downtown Store",
                Phone = "664-123-4567",
                Location = "123 Main St",
                Latitude = 32.5342,
                Longitude = -116.9765
            },
            new Store
            {
                Id = 1002,
                Name = "Eastside Market",
                Phone = "664-765-4321",
                Location = "456 Oak Ave",
                Latitude = 32.5489,
                Longitude = -116.8754
            },
        ];
        //End test
        
        return stores;
    }

    /// <summary>
    /// Returns the store with the specified id
    /// </summary>
    /// <param name="id">Store id</param>
    /// <returns></returns>
    public static Store Get(int id)
    {
        //Test
        Store s = new Store
        {
            Id = id,
            Name = "Test Store",
            Phone = "664-555-1234",
            Location = "789 Test Blvd",
            Latitude = 32.5342,
            Longitude = -116.9765
        };
        //End test
        return s;
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
