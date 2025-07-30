using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

public class Reading
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<Reading> _readingColl = MongoDbConnection.GetCollection<Reading>("Readings");
    
    #endregion
    
    #region properties
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonElement("date")]
    public DateTime Date { get; set; }
    
    [BsonElement("door_state")]
    public bool DoorState { get; set; }
    
    [BsonElement("temp")]
    public double Temperature { get; set; }
    
    [BsonElement("perc_humidity")]
    public int PercHumidity { get; set; }
    
    [BsonElement("latitude")]
    public double Latitude { get; set; }
    
    [BsonElement("longitude")]
    public double Longitude { get; set; }

    [BsonElement("IDTruck")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IDTruck { get; set; }

    #endregion
    
    #region class methods

    public static List<Reading> Get() 
    {
        return _readingColl.Find(r => true).ToList();
    }

    public static Reading Get(string id)
    {
        return _readingColl.Find(r => r.Id == id).FirstOrDefault();
    }
    public static List<Reading> GetByTruck(string truckId)
    {
        return _readingColl.Find(r => r.IDTruck == truckId).ToList();
    }

    public static Reading Insert(CreateReadingDto c, string truckId)
    {
        var reading = new Reading
        {
            Date = c.Date?? DateTime.Now,
            DoorState = c.DoorState,
            Latitude = c.Latitude,
            Longitude = c.Longitude,
            PercHumidity = c.Humidity,
            Temperature = c.Temperature,
            IDTruck = truckId,
        };

        return Insert(reading);
    }

    public static Reading Insert(Reading r)
    {
        try
        {
            if(string.IsNullOrEmpty(r.Id))
                r.Id = ObjectId.GenerateNewId().ToString();
            _readingColl.InsertOne(r);
            return r;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error inserting reading: "+e.Message);
        }
    }

    public static Reading Insert(string idTruck, CreateReadingDto c)
    {
        Reading r = new Reading
        {
            Date = DateTime.Now,
            IDTruck = idTruck,
            DoorState = c.DoorState,
            Latitude = c.Latitude,
            Longitude = c.Longitude,
            PercHumidity = c.Humidity,
            Temperature = c.Temperature,
        };
        
        return Insert(r);
    }
    
    #endregion
}
