using MongoDB.Driver;
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Order
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<Order> _orderColl = MongoDbConnection.GetCollection<Order>("Orders");
    private static IMongoCollection<Order> _orderLogColl = MongoDbConnection.GetCollection<Order>("ViewOrderLogs");
    
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

    #region constructors

    public Order()
    {
        Id = ObjectId.GenerateNewId().ToString();
        Date = DateTime.Now;
        DeliverDate = CalculateDeliverDate();
        IDStateOrder = "PO";
        IDStore = "";
        IDUser = "";
    }

    public Order(OrderDto dto)
    {
        Id = dto.Id;
        IDStore = dto.Store.Id;
        IDUser = dto.CreatedBy.Id;
        IDStateOrder = dto.State.Id;
        DeliverDate = dto.DeliverDate.ToDateTime(TimeOnly.MinValue);
        Date = dto.Date.ToDateTime(TimeOnly.MinValue);
    }
    public Order(ViewOrder v)
    {
        Id = v.Id;
        IDStore = v.IDStore;
        IDUser = v.IDUser;
        IDStateOrder = v.IDStateOrder;
        DeliverDate = v.DeliverDate.Date;
        Date = v.Date;
    }

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
    public static List<Order> GetPending(DateTime date)
    {
        return null;
    }
    public static List<Order> GetByRoute(string idRoute) {
        return  ViewOrder.GetByRoute(idRoute);
    }

    public static Order Insert(CreateOrderDto c)
    {
        DateTime date = c.Date ?? DateTime.Now;
        Order order = new Order
        {
            Date = date,
            IDUser = c.IDCreatedByUser,
            IDStore = c.IDStore,
            IDStateOrder = "PO",
        };
        order.DeliverDate = order.CalculateDeliverDate();
        OrderLog.Insert(order, date);
        return Insert(order);
    }

    public static Order Insert(Order o)
    {
        try
        {
            if(string.IsNullOrEmpty(o.Id))
                o.Id = ObjectId.GenerateNewId().ToString();
            o.DeliverDate = o.DeliverDate.Date;
            _orderColl.InsertOne(o);
            return o;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error inserting order: "+e);
            throw new Exception("Error inserting order: "+e);
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
        DateTime closerDate = this.Date.Date;
        while (closerDate.DayOfWeek != closerDay)
            closerDate = closerDate.AddDays(1);
        
        return closerDate.Date;
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

    public static Order GenerateOrder(DateTime? date)
    {
        date ??= DateTime.Now;
        Random rnd = new Random();
        
        //get admins and store that not ordered yet
        List<UserApp> users = UserApp.GetAdmin();
        List<Store> stores = Store.GetNotOrders();
        
        if(users.Count < 1)
            throw new Exception("No hay administradores");
        if(stores.Count < 1)
            throw new Exception("No hay tiendas sin ordenar hoy");
        
        //get a random store and admin
        UserApp admin = users[rnd.Next(0, users.Count-1)];
        Store store = stores[rnd.Next(0, stores.Count-1)];

        CreateOrderDto o = new CreateOrderDto
        {
            Date = date,
            IDCreatedByUser = admin.Id,
            IDStore = store.Id
        };
        
        return Order.Insert(o);
    }
    
    #endregion
}

public class ViewOrder : Order
{
    private static IMongoCollection<ViewOrder> _viewOrderColl = MongoDbConnection.GetCollection<ViewOrder>("ViewOrders");
    
    [BsonElement("IDRoute")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IDRoute { get; set; }
    public ViewOrder(){}
    public ViewOrder(Order o)
    {
        Id = o.Id;
        Date = o.Date;
        DeliverDate = o.DeliverDate;
        IDStateOrder = o.IDStateOrder;
        IDStore = o.IDStore;
        IDUser = o.IDUser;
    }
    public static List<Order> GetByRoute(string routeId)
    {
        var viewOrders= _viewOrderColl.Find(o => o.IDRoute == routeId).ToList();
        var orders = new List<Order>();
        foreach (var viewOrder in viewOrders)
        {
            orders.Add(new Order(viewOrder));
        }
        return orders;
    }
}