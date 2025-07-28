using MongoDB.Driver;
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Store
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<Store> _storeColl = MongoDbConnection.GetCollection<Store>("Stores");
    
    #endregion
    
    #region properties

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
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
    
    [BsonElement("active")]
    public bool Active { get; set; }

    #endregion
    
    #region class methods

    public static List<Store> Get() 
    {
        return _storeColl.Find(s => s.Active == true).ToList();       
    }
    public static List<Store> GetNotOrders() 
    {
        var ordersCollection = MongoDbConnection.GetCollection<Order>("Orders");

        var pipeline = new[]
        {
            new BsonDocument("$lookup", new BsonDocument
            {
                { "from", "Orders" },
                { "localField", "_id" },
                { "foreignField", "IDStore" },
                { "as", "orders" }
            }),
            new BsonDocument("$unwind", "$orders"),
            new BsonDocument("$match", new BsonDocument
            {
                { "orders.IDStateOrder", new BsonDocument("$ne", "PO") }
            }),
            new BsonDocument("$project", new BsonDocument
            {
                { "name", 1 },
                { "phone", 1 },
                { "location", 1 },
                { "latitude", 1 },
                { "longitude", 1 },
                { "active", 1 }
            })
        };

        return _storeColl.Aggregate<Store>(pipeline).ToList();
    }
    public static Store Get(string id)
    {
        return _storeColl.Find(s => s.Id == id).FirstOrDefault();       
    } 
    public static bool Ordered(string id)
    {
        var ordersCollection = MongoDbConnection.GetCollection<Order>("Orders");
        
        long orders = ordersCollection.Find(o => o.IDStore == id && o.IDStateOrder == "PO").CountDocuments();
        return orders > 0;
    }

    public static Store Insert(Store s)
    {
        try
        {
            if(string.IsNullOrEmpty(s.Id))
                s.Id = ObjectId.GenerateNewId().ToString();
            _storeColl.InsertOne(s);
            return s;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error inserting store: "+e.Message);
        }
    }
    
    #endregion
}
