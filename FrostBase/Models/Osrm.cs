using System.Text.Json;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

public class Osrm
{
    private static readonly IMongoCollection<Osrm> _osrmColl = MongoDbConnection.GetCollection<Osrm>("OsrmRoutes");
    
    #region properties

    private static readonly HttpClient _httpClient = new HttpClient();

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("startLatitude")]
    public double StartLatitude { get; set; }
    
    [BsonElement("startLongitude")]
    public double StartLongitude { get; set; }
    
    [BsonElement("endLatitude")]
    public double EndLatitude { get; set; }
    
    [BsonElement("endLongitude")]
    public double EndLongitude { get; set; }

    [BsonElement("distance")]
    public double Distance { get; set; }
    
    [BsonElement("duration")]
    public double Duration { get; set; }
    
    // [BsonElement("geometry")]
    // public string Geometry { get; set; }

    public Osrm() { }
    
    public Osrm(Location start, Location end)
    {
        StartLatitude = start.Latitude;
        StartLongitude = start.Longitude;
        EndLatitude = end.Latitude;
        EndLongitude = end.Longitude;
    }

    #endregion

    #region class methods

    public static Osrm Get(Location start, Location end)
    {
        double tolerance = 1e-5; // 0.00001
        //calculate the difference because double can round values
        var route = _osrmColl.Find(r => 
                    Math.Abs(r.StartLatitude - start.Latitude) < tolerance && 
                    Math.Abs(r.StartLongitude - start.Longitude) < tolerance && 
                    Math.Abs(r.EndLatitude - end.Latitude) < tolerance && 
                    Math.Abs(r.EndLongitude - end.Longitude) < tolerance
                );
        if (route.CountDocuments() > 0)
        {
            Console.WriteLine("||| Ruta encontrada en la base de datos |||");
            return route.FirstOrDefault();
        }

        Console.WriteLine("||| Insertando nueva ruta en la base de datos |||");
        return Insert(start, end);
    }

    private static Osrm Insert(Location start, Location end)
    {
        try
        {
            var osrm = GetRoute(start, end).Result;
            _osrmColl.InsertOne(osrm);
            return osrm;
        }
        catch (Exception e)
        {
            Console.WriteLine("osrm insert: "+e);
            throw new Exception("Error inserting osrm: "+e.Message);
        }
    }

    private static async Task<Osrm> GetRoute(Location start, Location end)
    {
        return await GetRoute(start.Latitude, start.Longitude, end.Latitude, end.Longitude);
    }

    private static async Task<Osrm> GetRoute(double startLat, double startLon, double endLat, double endLon)
    {
        var osrmRoute = new Osrm
        {
            StartLatitude = startLat,
            StartLongitude =   startLon,
            EndLatitude = endLat,
            EndLongitude = endLon,
        }; 
        var url = $"https://router.project-osrm.org/route/v1/driving/{startLon},{startLat};{endLon},{endLat}?overview=false";
        
        Console.WriteLine("OSRM URL: " + url);

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        
        // Deserialize into the OSRM API response model
        var osrmApiResponse = JsonSerializer.Deserialize<OsrmApiResponse>(json);

        var route = osrmApiResponse?.Routes?.FirstOrDefault();

        if (route == null)
        {
            Console.WriteLine("Respuesta OSRM sin rutas:");
            Console.WriteLine(json); // Imprime el JSON para debug
            throw new Exception("No se encontro una ruta valida");
        }

        osrmRoute.Distance = route.Distance;
        osrmRoute.Duration = route.Duration;
        
        return osrmRoute;
    }

    #endregion
}

// Define classes that match the OSRM API response structure
public class OsrmApiResponse
{
    [JsonPropertyName("code")]
    public string Code { get; set; }
    [JsonPropertyName("routes")]
    public List<OsrmRoute> Routes { get; set; }
}

public class OsrmRoute
{
    [JsonPropertyName("distance")]
    public double Distance { get; set; }

    [JsonPropertyName("duration")]
    public double Duration { get; set; }

    [JsonPropertyName("geometry")]
    public string Geometry { get; set; }
}