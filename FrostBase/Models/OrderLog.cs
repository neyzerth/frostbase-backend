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
    public DateTime Date { get; set; }
    
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

    public OrderLog()
    { }

    public OrderLog(Order o, DateTime date)
    {
        Id = ObjectId.GenerateNewId().ToString();
        Date = date;
        IDOrder = o.Id;
        IDStateOrder = o.IDStateOrder;
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
            Console.WriteLine("Order log insert: "+e);
            throw new Exception("Error inserting order log: "+e.Message);
        }
    }

    public static List<OrderLog> InsertMany(List<Order> orders)
    {
        var logs = new List<OrderLog>();
        foreach (var order in orders)
        {
            logs.Add(new OrderLog(order, order.Date));
        }
        
        return InsertMany(logs);
    }

    public static List<OrderLog> InsertMany(List<OrderLog> logs)
    {
        try
        {
            _orderLogColl.InsertMany(logs);
            return logs;
        }
        catch (Exception e)
        {
            Console.WriteLine("Order log insert: "+e);
            throw new Exception("Error inserting many order log: "+e.Message);
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
                Date = date ?? DateTime.Now
            };
            return Insert(log);       
        }
        catch (Exception e)
        {
            Console.WriteLine("Order log insert: "+e);
            throw new Exception("Error inserting log of order "+ o.Id+": "+e.Message);
        }
    }
    
}