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
    public DateTime DeliverDate { get; set; }

    [BsonElement("IDCreatedByUser")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IDUser { get; set; }

    [BsonElement("IDStore")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IDStore { get; set; }
    
    [BsonElement("IDStateOrder")]
    public string IDStateOrder { get; set; }

    #endregion
    
    #region database

    public static List<Order> Get() 
    {
        return _orderColl.Find(_ => true).ToList();
    }
    
    public static Order Get(string id)
    {
        return _orderColl.Find(o => o.Id == id).FirstOrDefault();
    }
    public static List<Order> GetPending()
    {
        return _orderColl.Find(o => o.IDStateOrder == "PO" || o.IDStateOrder == "LO").ToList();;
    }

    public static Order Insert(CreateOrderDto c)
    {
        DateTime date = c.Date == null ? DateTime.Now : c.Date.Value;
        Order order = new Order
        {
            Date = date,
            IDUser = c.IDCreatedByUser,
            IDStore = c.IDStore,
            IDStateOrder = "PO",
        };
        
        if(c.Date == null) order.Date = DateTime.Now;
        order.DeliverDate = order.CalculateDeliverDate();
        
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

    #region logic

    private DateTime CalculateDeliverDate()
    {
        try
        {
            //filtro para encontrar rutas con la tienda de la orden
            var filter = Builders<Route>.Filter.And(
                Builders<Route>.Filter.Eq("stores.IDStore", IDStore),
                Builders<Route>.Filter.Eq(r => r.Active, true)
            );
            Route matchRoute = MongoDbConnection.GetCollection<Route>("Routes").
                Find(filter).FirstOrDefault();
            
            return CalculateCloserDate(matchRoute.DeliverDays);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return Date.AddDays(1);
        }
    }

    private DateTime CalculateCloserDate(List<int> deliverDays)
    {
        //dia de semana mas cercano de entrega
        DayOfWeek closerDay = CloserDayOfWeek(deliverDays);
        
        //calcular la fecha de entrega
        DateTime closerDate = this.Date;
        while (closerDate.DayOfWeek != closerDay)
            closerDate = closerDate.AddDays(1);
        
        return closerDate;
    }

    private DayOfWeek CloserDayOfWeek(List<int> days)
    {
        //un dia de semana nunca puede ser 8
        int closerDay = 8;
        int now = (int)Date.DayOfWeek;
        
        //buscar el siguiente dia mas cercano
        foreach (int day in days)
        {
            //contemplar solo los dias siguientes
            if (now <= day)
                //tomar el dia menor
                closerDay = int.Min(day, closerDay);
        }

        //si no paso niguna validacion, que agarre el menor dia
        if (closerDay < 0)
            closerDay = days.Min();

        return (DayOfWeek)closerDay;
    }

    #endregion
    
    #region simulator

    public static Order GenerateOrder()
    {
        //get a random admin
        Random rnd = new Random();
        List<UserApp> users = UserApp.GetAdmin();
        UserApp admin = users[rnd.Next(0, users.Count-1)];
        
        //get a random store that not ordered yet
        List<Store> stores = Store.GetNotOrders();
        Store store = stores[rnd.Next(0, stores.Count-1)];

        CreateOrderDto o = new CreateOrderDto
        {
            Date = DateTime.Now,
            IDCreatedByUser = admin.Id,
            IDStore = store.Id
        };
        
        return Order.Insert(o);
    }
    
    #endregion
}