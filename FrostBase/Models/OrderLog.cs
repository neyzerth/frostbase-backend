using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

public class OrderLog
{
    
    public static IMongoCollection<OrderLog> _orderLogColl = MongoDbConnection.GetCollection<OrderLog>("OrderLogs");
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonElement("date")]
    public DateTime date { get; set; }
    
    [BsonElement("IDOrder")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IDOrder { get; set; }
    
    [BsonElement("IDStateOrder")]
    public string IDStateOrder { get; set; }
    
    public static List<OrderLog> Get()
    {
        return _orderLogColl.Find(o => true).ToList();
    }
    
    public static OrderLog Get(string id)
    {
        return _orderLogColl.Find(o => o.Id == id).FirstOrDefault();
    }

    public static List<OrderLog> GetByOrder(string id)
    {
        return _orderLogColl.Find(o => o.IDOrder == id).ToList();      
    }
    
    public static OrderLog Insert(OrderLog o)
    {
        try
        {
            if(string.IsNullOrEmpty(o.Id))
                o.Id = ObjectId.GenerateNewId().ToString();
            _orderLogColl.InsertOne(o);
            return o;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error inserting order log: "+e.Message);
        }
    }

    public static OrderLog Insert(Order o, DateTime? date)
    {
        try
        {
            var log = new OrderLog
            {
                IDOrder = o.Id,
                IDStateOrder = o.IDStateOrder,
                date = date ?? DateTime.Now
            };
            return Insert(log);       
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error inserting log of order "+ o.Id+": "+e.Message);
        }
    }
}