using MongoDB.Driver;
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class DoorEvent
{
    //Sql or mongo statements
    private static IMongoCollection<DoorEvent> _doorEventColl = MongoDbConnection.GetCollection<DoorEvent>("DoorEvents");
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("state")]
    public bool State { get; set; }
    
    [BsonElement("time_opened")]
    public TimeSpan TimeOpened { get; set; }
    
    [BsonElement("IDTruck")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IDTruck { get; set; }

    public static List<DoorEvent> Get() 
    {
        return _doorEventColl.Find(d => true).ToList();
    }

    public static DoorEvent Get(string id)
    {
        return _doorEventColl.Find(d => d.Id == id).FirstOrDefault();       
    }

    public static DoorEvent Insert(DoorEvent d)
    {
        try
        {
            if(string.IsNullOrEmpty(d.Id))
                d.Id = ObjectId.GenerateNewId().ToString();
            _doorEventColl.InsertOne(d);
            return d;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error inserting door event: "+e.Message);
        }
    }
}
