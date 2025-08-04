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
    public static List<Store> GetNotOrders(DateTime? date = null)
    {
        DateTime datetime = date ?? DateTime.Now;
        var pipeline = new List<BsonDocument>()
        {
            new BsonDocument("$lookup",
                new BsonDocument
                {
                    { "from", "Orders" },
                    {
                        "let",
                        new BsonDocument("storeId", "$_id")
                    },
                    {
                        "pipeline",
                        new BsonArray
                        {
                            new BsonDocument("$match",
                                new BsonDocument("$expr",
                                    new BsonDocument("$and",
                                        new BsonArray
                                        {
                                            new BsonDocument("$eq",
                                                new BsonArray
                                                {
                                                    "$IDStore",
                                                    "$$storeId"
                                                }),
                                            new BsonDocument("$lte",
                                                new BsonArray
                                                {
                                                    "$delivered",
                                                    datetime
                                                }),
                                            new BsonDocument("$eq",
                                                new BsonArray
                                                {
                                                    new BsonDocument("$dateToString",
                                                        new BsonDocument
                                                        {
                                                            { "format", "%Y-%m-%d" },
                                                            { "date", "$delivered" }
                                                        }),
                                                    new BsonDocument("$dateToString",
                                                        new BsonDocument
                                                        {
                                                            { "format", "%Y-%m-%d" },
                                                            {
                                                                "date",
                                                                datetime
                                                            }
                                                        })
                                                })
                                        })))
                        }
                    },
                    { "as", "ordersMatch" }
                }),
            new BsonDocument("$match",
                new BsonDocument("ordersMatch",
                    new BsonDocument("$size", 0))),
            new BsonDocument("$project",
                new BsonDocument("ordersMatch", 0))
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

    public static Store Insert(CreateStoreDto c)
    {
        var store = new Store
        {
            Name = c.Name,
            Phone = c.Phone,
            Location = c.Location.Address,
            Latitude = c.Location.Latitude,
            Longitude = c.Location.Latitude,
            Active = true
        };

        return Insert(store);
    }

    public static Store Update(UpdateStoreDto u)
    {
        var store = new Store()
        {
            Id = u.Id,
            Name = u.Name,
            Phone = u.Phone,
            Location = u.Location.Address,
            Latitude = u.Location.Latitude,
            Longitude = u.Location.Latitude,
            Active = u.Active,
        };
        
        return Update(store);
    }

    public static Store Update(Store s)
    {
        try
        {
            var filter = Builders<Store>.Filter.Eq(t => t.Id, s.Id);
            var update = Builders<Store>.Update
                .Set(t => t.Name, s.Name)
                .Set(t => t.Phone, s.Phone)
                .Set(t => t.Location, s.Location)
                .Set(t => t.Latitude, s.Latitude)
                .Set(t => t.Longitude, s.Longitude)
                .Set(t => t.Active, s.Active);

            var options = new FindOneAndUpdateOptions<Store>
            {
                ReturnDocument = ReturnDocument.After // Retorna el documento ya actualizado
            };

            return _storeColl.FindOneAndUpdate(filter, update, options);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error updating store: " + e.Message);
        }
    }

    public static Store Delete(string id)
    {
        try
        {
            var filter = Builders<Store>.Filter.Eq(t => t.Id, id);
            var update = Builders<Store>.Update
                .Set(t => t.Active, false);

            var options = new FindOneAndUpdateOptions<Store>
            {
                ReturnDocument = ReturnDocument.After // Retorna el documento ya actualizado
            };

            return _storeColl.FindOneAndUpdate(filter, update, options);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error updating store: " + e.Message);
        }
    }
    
    #endregion
}
