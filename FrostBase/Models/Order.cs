using MongoDB.Driver;
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Order
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<Order> _orderColl = MongoDbConnection.GetCollection<Order>("Orders");
    
    #endregion
    
    #region properties
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("date")]
    public DateTime Date { get; set; }

    [BsonElement("delivered")]
    public DateTime? Delivered { get; set; }

    [BsonElement("IDCreatedByUser")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IDUser { get; set; }

    [BsonElement("IDStore")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IDStore { get; set; }
    
    [BsonElement("IDStateOrder")]
    public string IDStateOrder { get; set; }

    #endregion
    
    #region class methods

    public static List<Order> Get() 
    {
        return _orderColl.Find(_ => true).ToList();
    }
    
    public static Order Get(string id)
    {
        return _orderColl.Find(o => o.Id == id).FirstOrDefault();
    }

    public static Order Insert(CreateOrderDto c)
    {
        Order order = new Order
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Date = DateTime.Now,
            Delivered = null,
            IDUser = c.IDUser,
            IDStore = c.IDStore,
            IDStateOrder = "pendi",
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