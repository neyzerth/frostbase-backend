using MongoDB.Driver;
using System;
using MongoDB.Bson.Serialization.Attributes;

public class Order
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<Order> _orderColl = MongoDbConnection.GetCollection<Order>("orders");
    
    #endregion
    
    #region attributes
    
    private int _id;
    private DateTime _date;
    private DateTime? _delivered;
    private UserApp _user;
    private Store _store;

    #endregion

    #region properties
    
    [BsonId]
    public int Id
    {
        get => _id;
        set => _id = value;
    }
    
    [BsonElement("date")]
    public DateTime Date
    {
        get => _date;
        set => _date = value;
    }
    
    [BsonElement("delivered")]
    public DateTime? Delivered
    {
        get => _delivered;
        set => _delivered = value;
    }
    
    [BsonElement("user")]
    public UserApp User
    {
        get => _user;
        set => _user = value;
    }
    
    [BsonElement("store")]
    public Store Store
    {
        get => _store;
        set => _store = value;
    }

    #endregion
    
    #region class methods

    /// <summary>
    /// Returns a list of all orders
    /// </summary>
    /// <returns></returns>
    public static List<Order> Get() 
    {
        //Test
        List<Order> orders =
        [
            new Order
            {
                Id = 1001,
                Date = DateTime.Now.AddDays(-2),
                Delivered = DateTime.Now.AddDays(-1),
                User = UserApp.Get(1001),
                Store = Store.Get(1001)
            },
            new Order
            {
                Id = 1002,
                Date = DateTime.Now.AddDays(-1),
                Delivered = null,
                User = UserApp.Get(1002),
                Store = Store.Get(1002)
            },
        ];
        //End test
        
        return orders;
    }

    /// <summary>
    /// Returns the order with the specified id
    /// </summary>
    /// <param name="id">Order id</param>
    /// <returns></returns>
    public static Order Get(int id)
    {
        //Test
        Order o = new Order
        {
            Id = id,
            Date = DateTime.Now.AddDays(-1),
            Delivered = null,
            User = UserApp.Get(1001),
            Store = Store.Get(1001)
        };
        //End test
        return o;
    }

    public static bool Insert(Order o)
    {
        try
        {
            _orderColl.InsertOne(o);
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
