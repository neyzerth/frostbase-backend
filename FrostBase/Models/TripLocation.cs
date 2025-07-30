using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

public class TripLocation
{
    private static IMongoCollection<Trip> _tripColl = 
        MongoDbConnection.GetCollection<Trip>("Trips");
    
    [BsonElement("truck")]
    public Truck Truck { get; set; }
    
    [BsonElement("IDTrip")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IDTrip { get; set; }
    
    [BsonElement("latitude")]
    public double Latitude { get; set; }
    
    [BsonElement("longitude")]
    public double Longitude { get; set; }
    
    [BsonElement("date")]
    public DateTime Date { get; set; }

    public static List<TripLocation> GetTruckLocations(DateTime date)
    {
        string dateOnly = date.ToString("yyyy-MM-dd");

        var pipeline = new List<BsonDocument>
        {
            new BsonDocument("$match",
                new BsonDocument("$expr",
                    new BsonDocument("$eq",
                        new BsonArray
                        {
                            new BsonDocument("$dateToString",
                                new BsonDocument
                                {
                                    { "format", "%Y-%m-%d" },
                                    { "date", "$start_time" }
                                }),
                            dateOnly
                        }))),
            new BsonDocument("$lookup",
                new BsonDocument
                {
                    { "from", "Readings" },
                    { "localField", "IDTruck" },
                    { "foreignField", "IDTruck" },
                    { "as", "readings" }
                }),
            new BsonDocument("$unwind", "$readings"),
            new BsonDocument("$match",
                new BsonDocument("$expr",
                    new BsonDocument("$eq",
                        new BsonArray
                        {
                            new BsonDocument("$dateToString",
                                new BsonDocument
                                {
                                    { "format", "%Y-%m-%d" },
                                    { "date", "$readings.date" }
                                }),
                            dateOnly
                        }))),
            new BsonDocument("$sort",
                new BsonDocument
                {
                    { "_id", 1 },
                    { "readings.date", -1 }
                }),
            new BsonDocument("$group",
                new BsonDocument
                {
                    { "_id", "$_id" },
                    {
                        "truck",
                        new BsonDocument("$first", "$truck")
                    },
                    {
                        "IDTruck",
                        new BsonDocument("$first", "$IDTruck")
                    },
                    {
                        "latitude",
                        new BsonDocument("$first", "$readings.latitude")
                    },
                    {
                        "longitude",
                        new BsonDocument("$first", "$readings.longitude")
                    },
                    {
                        "date",
                        new BsonDocument("$first", "$readings.date")
                    }
                }),
            new BsonDocument("$lookup",
                new BsonDocument
                {
                    { "from", "Trucks" },
                    { "localField", "IDTruck" },
                    { "foreignField", "_id" },
                    { "as", "truck" }
                }),
            new BsonDocument("$unwind", "$truck"),
            new BsonDocument("$project",
                new BsonDocument
                {
                    { "_id", 0 },
                    { "IDTrip", "$_id" },
                    { "truck", 1 },
                    { "latitude", 1 },
                    { "longitude", 1 },
                    { "date", 1 }
                })
        };
        
        var pipelineDef = PipelineDefinition<Trip, TripLocation>.Create(pipeline);

        return _tripColl.Aggregate<TripLocation>(pipelineDef).ToList();
    }
}