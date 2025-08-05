using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
public class Truck
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<Truck> _truckColl = 
        MongoDbConnection.GetCollection<Truck>("Trucks");
    
    #endregion

    #region properties

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonElement("brand")]
    public string Brand { get; set; }
    
    [BsonElement("model")]
    public string Model { get; set; }
    
    [BsonElement("license_plate")]
    public string LicensePlate { get; set; }
    
    [BsonElement("IDStateTruck")]
    public string IDStateTruck { get; set; }
    
    #endregion
    
    #region class methods

    public static List<Truck> Get() 
    {
        return _truckColl.Find(t => true).ToList();
    }
    
    public static List<Truck> GetAvailable() 
    {
        return _truckColl.Find(t => 
            t.IDStateTruck == "AV").ToList();
    }
    public static List<Truck> GetAvailable(DateTime date) 
    {
        var logs = TruckLog.GetByDate(date);
        var trucks = new List<Truck>();
        
        foreach (var l in logs)
        {
            trucks.Add(Truck.Get(l.Id));
        }
        return trucks;
    }
    
    public static Truck Get(string id)
    {
        return _truckColl.Find(t => t.Id == id).FirstOrDefault();
    }
    public static Truck GetAvailable(string id)
    {
        return _truckColl.Find(t => 
                t.Id == id && t.IDStateTruck == "AV"
            ).FirstOrDefault();
    }

    public static Truck Insert(CreateTruckDto c)
    {
        Truck t = new Truck
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Brand = c.Brand,
            Model = c.Model,
            LicensePlate = c.LicensePlate,
            IDStateTruck = "AV"
        };
        
        TruckLog.Insert(t, DateTime.Now);
        return Insert(t);   
    }
    public static Truck Insert(CreateTruckDto c, DateTime date)
    {
        Truck t = new Truck
        {
            Brand = c.Brand,
            Model = c.Model,
            LicensePlate = c.LicensePlate,
            IDStateTruck = "AV"
        };
        
        TruckLog.Insert(t, date);
        return Insert(t);   
    }

    public static Truck Insert(Truck t)
    {
        try
        {
            if(string.IsNullOrEmpty(t.Id))
                t.Id = ObjectId.GenerateNewId().ToString();
            _truckColl.InsertOne(t);
            return t;
        }
        catch (Exception e)
        {
            Console.WriteLine("Truck insert: "+e);
            throw new Exception("Error inserting truck: "+e.Message);
        }
    }
    
    public static Truck Update(Truck updatedTruck, DateTime? date = null)
    {
        date??= DateTime.Now;
        try
        {
            var filter = Builders<Truck>.Filter.Eq(t => t.Id, updatedTruck.Id);
            var update = Builders<Truck>.Update
                .Set(t => t.Brand, updatedTruck.Brand)
                .Set(t => t.Model, updatedTruck.Model)
                .Set(t => t.LicensePlate, updatedTruck.LicensePlate)
                .Set(t => t.IDStateTruck, updatedTruck.IDStateTruck);

            var options = new FindOneAndUpdateOptions<Truck>
            {
                ReturnDocument = ReturnDocument.After // Retorna el documento ya actualizado
            };

            TruckLog.Insert(updatedTruck, date);
            return _truckColl.FindOneAndUpdate(filter, update, options);
        }
        catch (Exception e)
        {
            Console.WriteLine("Truck insert: "+e);
            throw new Exception("Error updating truck: " + e.Message);
        }
    }

    public static Truck Update(UpdateTruckDto updatedTruck)
    {
        var truck = new Truck
        {
            Id = updatedTruck.Id,
            Brand = updatedTruck.Brand,
            Model = updatedTruck.Model,
            LicensePlate = updatedTruck.LicensePlate,
            IDStateTruck = updatedTruck.IDStateTruck
        };
        return Update(truck);
    }

    public static Truck Delete(string id)
    {
        try
        {
            var filter = Builders<Truck>.Filter.Eq(t => t.Id, id);
            var update = Builders<Truck>.Update
                .Set(t => t.IDStateTruck, "OS");

            var options = new FindOneAndUpdateOptions<Truck>
            {
                ReturnDocument = ReturnDocument.After // Retorna el documento ya actualizado
            };

            return _truckColl.FindOneAndUpdate(filter, update, options);
        }
        catch (Exception e)
        {
            Console.WriteLine("Truck insert: "+e);
            throw new Exception("Error deleting truck: " + e.Message);
        }
    }



    #endregion
}