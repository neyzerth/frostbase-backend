using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

public class Route
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<Route> _routeColl = MongoDbConnection.GetCollection<Route>("Routes");
    
    #endregion
    
    #region properties

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }
    
    [BsonElement("deliverDays")]
    public List<int> DeliverDays{ get; set; }
    
    [BsonElement("IDAssignedUser")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IDUser { get; set; }
    
    [BsonElement("active")]
    public bool Active { get; set; }
    
    [BsonElement("stores")]
    public List<RouteStore> Stores { get; set; }

    #endregion
    
    #region class methods

    public static List<Route> Get() 
    {
        return _routeColl.Find(r => r.Active == true).ToList();
    }
    
    public static Route Get(string id)
    {
        return _routeColl.Find(r => r.Id == id).FirstOrDefault();
    }

    public static Route Insert(Route r)
    {
        try
        {
            if(string.IsNullOrEmpty(r.Id))
                r.Id = ObjectId.GenerateNewId().ToString();
            _routeColl.InsertOne(r);
            return r;
        }
        catch (Exception e)
        {
            Console.WriteLine("Route insert: "+e);
            throw new Exception("Error inserting route: "+e.Message);
        }
    }

    public static Route Insert(CreateRouteDto c)
    {
        var route = new Route
        {
            Id = ObjectId.GenerateNewId().ToString(),
            DeliverDays = c.DeliverDays,
            IDUser = c.IDCreatedBy,
            Stores = c.stores,
            Active = true,
        };
        
        return Insert(route);
    }
    public static Route Update(UpdateRouteDto update)
    {
        var route = new Route()
        {
            Id = update.Id,
            Name = update.Name,
            DeliverDays = update.DeliverDays,
            IDUser = update.IDCreatedBy,
            Stores = update.Stores,
            Active = true,
        };

        return Update(route);
    }
    public static Route Update(Route r)
    {
        try
        {
            var filter = Builders<Route>.Filter.Eq(o => o.Id, r.Id);
            var update = Builders<Route>.Update
                .Set(o => o.Name, r.Name)
                .Set(o => o.DeliverDays, r.DeliverDays)
                .Set(o => o.Active, r.Active)
                .Set(o => o.Stores, r.Stores)
                .Set(o => o.IDUser, r.IDUser);

            var options = new FindOneAndUpdateOptions<Route>
            {
                ReturnDocument = ReturnDocument.After // Retorna el documento ya actualizado
            };

            return _routeColl.FindOneAndUpdate(filter, update, options);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error updating route"+e);
            throw new Exception("Error updating route: "+e.Message);
        }
    }

    public static Route Delete(string id)
    {
        try
        {
            var filter = Builders<Route>.Filter.Eq(o => o.Id, id);
            var update = Builders<Route>.Update
                .Set(o => o.Active, false);

            var options = new FindOneAndUpdateOptions<Route>
            {
                ReturnDocument = ReturnDocument.After // Retorna el documento ya actualizado
            };

            return _routeColl.FindOneAndUpdate(filter, update, options);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error deleting route"+e);
            throw new Exception("Error deleting route: "+e.Message);
        }
    }
    
    public static List<Route> GetByDate(DateTime date)
    {
        int day = (int)date.DayOfWeek;
        return GetByDay(day);
    }
    
    public static List<Route> GetByDay(int day)
    {
        return _routeColl.Find(r => r.DeliverDays.Contains(day)).ToList();
    }
    
    #endregion
}
