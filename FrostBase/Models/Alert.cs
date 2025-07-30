using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Alert
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<Alert> _alertColl = MongoDbConnection.GetCollection<Alert>("Alerts");
    
    #endregion
    
    #region properties
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id
    { get; set; }
    
    [BsonElement("state")]
    public bool State { get; set; }
    
    [BsonElement("date")] 
    public DateTime Date { get; set; }
    
    [BsonElement("IDAlertTypes")] 
    public string IDAlertType { get; set; }

    [BsonElement("IDReading")] 
    [BsonRepresentation(BsonType.ObjectId)]
    public string IDReading { get; set; }

    #endregion
    
    #region class methods

    public static List<Alert> Get()
    {
        return _alertColl.Find(a => true).ToList();
    }

    public static Alert Get(string id)
    {
        return _alertColl.Find(a => a.Id == id).FirstOrDefault();
    }
    
    public static List<Alert> GetByTruck(string truckId)
    {
        var pipeline = new List<BsonDocument>()
        {
            new BsonDocument("$lookup",
                new BsonDocument
                {
                    { "from", "Readings" },
                    { "localField", "IDReading" },
                    { "foreignField", "_id" },
                    { "as", "reading" }
                }),
            new BsonDocument("$unwind", "$reading"),
            new BsonDocument("$match",
                new BsonDocument("reading.IDTruck",
                    new ObjectId(truckId))),
            new BsonDocument("$project",
                new BsonDocument("reading", 0))
        };
        
        return _alertColl.Aggregate<Alert>(pipeline).ToList();
    }

    public static Alert Insert(Alert a)
    {
        try
        {
            if(string.IsNullOrEmpty(a.Id))
                a.Id = ObjectId.GenerateNewId().ToString();
            _alertColl.InsertOne(a);
            return a;
        }
        catch (Exception e)
        {
            Console.WriteLine("Alert insert: "+e);
            throw new Exception("Error inserting alert: "+e.Message);
        }
    }

    public static Alert Update(UpdateAlertDto u)
    {
        var alert = new Alert
        {
            Id = u.Id,
            State = u.State,
            Date = u.Date,
            IDAlertType = u.IDAlertType,
            IDReading = u.IDReading
        };
        return Update(alert);
    }

    public static Alert Update(Alert updatedAlert)
    {
        try
        {
            var filter = Builders<Alert>.Filter.Eq(a => a.Id, updatedAlert.Id);
            var update = Builders<Alert>.Update
                .Set(a => a.State, updatedAlert.State)
                .Set(a => a.Date, updatedAlert.Date)
                .Set(a => a.IDAlertType, updatedAlert.IDAlertType)
                .Set(a => a.IDReading, updatedAlert.IDReading);


            var options = new FindOneAndUpdateOptions<Alert>
            {
                ReturnDocument = ReturnDocument.After // Retorna el documento ya actualizado
            };

            return _alertColl.FindOneAndUpdate(filter, update, options);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error updating alert: " + e.Message);
        }
    }
    
    public static Alert Insert(CreateAlertDto a)
    {
        DateTime date = a.Date ?? DateTime.Now;

        Alert alert = new Alert
        {
            State = a.State,
            Date = date,
            IDAlertType = a.AlertType,
            IDReading = a.Reading
        };

        return Insert(alert);
    }
    
    #endregion
}
