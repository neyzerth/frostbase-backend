using MongoDB.Driver;
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Order
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<Order> _orderColl = MongoDbConnection.GetCollection<Order>("orders");
    
    #endregion
    
    #region properties
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("date")]
    public DateTime Date { get; set; }

    [BsonElement("delivered")]
    public DateTime? Delivered { get; set; }

    [BsonElement("IDUser")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IDUser { get; set; }

    [BsonElement("IDStore")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IDStore { get; set; }

    #endregion
    
    #region class methods

    public static List<Order> Get() 
    {
        //Test
        List<Order> orders =
        [
            new Order
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Date = DateTime.Now.AddDays(-2),
                Delivered = DateTime.Now.AddDays(-1),
                IDUser = ObjectId.GenerateNewId().ToString(),
                IDStore = ObjectId.GenerateNewId().ToString()
            },
            new Order
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Date = DateTime.Now.AddDays(-1),
                Delivered = null,
                IDUser = ObjectId.GenerateNewId().ToString(),
                IDStore = ObjectId.GenerateNewId().ToString()
            },
        ];
        //End test
        
        return orders;
    }

    public static Order Get(string id)
    {
        //Test
        Order o = new Order
        {
            Id = id,
            Date = DateTime.Now.AddDays(-1),
            Delivered = null,
            IDUser = ObjectId.GenerateNewId().ToString(),
            IDStore = ObjectId.GenerateNewId().ToString()
        };
        //End test
        return o;
    }

    public static Order Insert(CreateOrderDto c)
    {
        Order order = new Order
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Date = DateTime.Now,
            Delivered = null,
            IDUser = c.IDUser,
            IDStore = c.IDStore
        };
        return Insert(order);
    }

    public static Order Insert(Order o)
    {
        try
        {
            _orderColl.InsertOne(o);
            return o;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    #endregion
}