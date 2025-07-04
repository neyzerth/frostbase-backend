using MongoDB.Driver;
using MongoDB.Bson;

public class MongoDbConnection
{
    #region variables

    private static string _connectionString =
        Config.Configuration.MongoDB.ConnectionString;
    
    private static string _localConnString =
        "mongodb://" + Config.Configuration.MongoDB.Server + ":" + Config.Configuration.MongoDB.Port;

    private static string _databaseName = Config.Configuration.MongoDB.Database;

    #endregion

    #region class methods

    private static IMongoDatabase GetDatabase()
    {
        try
        {
            // Create a mongo client
            MongoClient client = new MongoClient(_connectionString);
            // get the database
            return client.GetDatabase(_databaseName);
        }
        catch (MongoConfigurationException e)
        {
            Console.WriteLine("CONFIGURATION EXCEPTION: " + e);
        }
        catch (MongoConnectionException e)
        {
            Console.WriteLine("CONNECTION EXCEPTION: " + e);
        }
        catch (Exception e)
        {
            Console.WriteLine("OTHER EXCEPTION: " + e);
        }
        return null;
    }


    // <T> is type, we can use any type with this collection (user, truck, etc)
    public static IMongoCollection<T> GetCollection<T>(string collection)
    {
        // connection
        IMongoDatabase db = GetDatabase();
        //return the colecction
        return db.GetCollection<T>(collection);
    }

    public static List<T> Find<T>(string collection)
    {
        // Use FilterDefinition<T>.Empty to get all documents
        return GetCollection<T>(collection).Find(FilterDefinition<T>.Empty).ToList();
    }
    public static List<BsonDocument> Find(string collection, BsonDocument? filter)
    {
        // use find() in a document with BsonDocument filter
        return GetCollection<BsonDocument>(collection).Find(filter).ToList();
    }

    #endregion
}