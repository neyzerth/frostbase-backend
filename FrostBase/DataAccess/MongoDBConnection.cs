using MongoDB.Driver;
using System;
using MongoDB.Bson;

public class MongoDbConnection
{
    #region variables

    private static string connectionString =
        "mongodb://" + Config.Configuration.MongoDB.Server + ":" + Config.Configuration.MongoDB.Port;

    private static string databaseName = Config.Configuration.MongoDB.Database;

    #endregion

    #region class methods

    private static IMongoDatabase GetDatabase()
    {
        try
        {
            // Crear cliente MongoDB
            MongoClient client = new MongoClient(connectionString);
            // Obtener base de datos
            return client.GetDatabase(databaseName);
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


    private static IMongoCollection<BsonDocument> GetCollection(string collection)
    {
        IMongoDatabase db = GetDatabase();
        return db.GetCollection<BsonDocument>(collection);
    }

    private static List<BsonDocument> Find(string collection, BsonDocument filter)
    {
        return GetCollection(collection).Find(filter).ToList();
    }

    #endregion
}