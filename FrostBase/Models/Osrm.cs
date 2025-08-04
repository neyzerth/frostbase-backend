using System.Text.Json;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

public class Osrm
{
    private static readonly IMongoCollection<Osrm> _osrmColl = MongoDbConnection.GetCollection<Osrm>("OsrmRoutes");
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

    [BsonElement("geometry")]
    public List<Location> Geometry { get; set; } = new List<Location>();

    public Osrm() { }

    public Osrm(Location start, Location end)
    {
        StartLatitude = start.Latitude;
        StartLongitude = start.Longitude;
        EndLatitude = end.Latitude;
        EndLongitude = end.Longitude;
    }

    public static Osrm Get(Location start, Location end)
    {
        double tolerance = 1e-5;
        var route = _osrmColl.Find(r =>
            Math.Abs(r.StartLatitude - start.Latitude) < tolerance &&
            Math.Abs(r.StartLongitude - start.Longitude) < tolerance &&
            Math.Abs(r.EndLatitude - end.Latitude) < tolerance &&
            Math.Abs(r.EndLongitude - end.Longitude) < tolerance
        );

        if (route.CountDocuments() > 0)
        {
            var dbRoute = route.FirstOrDefault();
            if (dbRoute.Geometry.Count > 0)
            {
                Console.WriteLine("||| Ruta encontrada en la base de datos |||");
                return dbRoute;
            }
        }

        Console.WriteLine("||| Insertando nueva ruta en la base de datos |||");
        return Insert(start, end);
    }

    private static Osrm Insert(Location start, Location end)
    {
        try
        {
            var osrm = GetRoute(start, end).Result;

            var filter = Builders<Osrm>.Filter.And(
                Builders<Osrm>.Filter.Eq(r => r.StartLatitude, osrm.StartLatitude),
                Builders<Osrm>.Filter.Eq(r => r.StartLongitude, osrm.StartLongitude),
                Builders<Osrm>.Filter.Eq(r => r.EndLatitude, osrm.EndLatitude),
                Builders<Osrm>.Filter.Eq(r => r.EndLongitude, osrm.EndLongitude)
            );

            var update = Builders<Osrm>.Update
                .Set(r => r.Distance, osrm.Distance)
                .Set(r => r.Duration, osrm.Duration)
                .Set(r => r.Geometry, osrm.Geometry);

            var options = new UpdateOptions { IsUpsert = true };

            _osrmColl.UpdateOne(filter, update, options);

            return osrm;
        }
        catch (Exception e)
        {
            Console.WriteLine("osrm upsert: " + e);
            throw new Exception("Error upserting osrm: " + e.Message);
        }
    }


    public static Location MoveTowards(Location A, Location B, double step)
    {
        double newLat = A.Latitude + (B.Latitude - A.Latitude) * step;
        double newLon = A.Longitude + (B.Longitude - A.Longitude) * step;

        return new Location { Latitude = newLat, Longitude = newLon };
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
            StartLongitude = startLon,
            EndLatitude = endLat,
            EndLongitude = endLon,
        };

        var url = $"https://router.project-osrm.org/route/v1/driving/{startLon},{startLat};{endLon},{endLat}?overview=full&geometries=geojson";
        Console.WriteLine("OSRM URL: " + url);

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        var osrmApiResponse = JsonSerializer.Deserialize<OsrmApiResponse>(json);

        var route = osrmApiResponse?.Routes?.FirstOrDefault();

        if (route == null)
        {
            Console.WriteLine("Respuesta OSRM sin rutas:");
            Console.WriteLine(json);
            throw new Exception("No se encontró una ruta válida");
        }

        osrmRoute.Distance = route.Distance;
        osrmRoute.Duration = route.Duration;

        if (route.Geometry?.Coordinates != null)
        {
            foreach (var coord in route.Geometry.Coordinates)
            {
                if (coord.Count >= 2)
                {
                    osrmRoute.Geometry.Add(new Location
                    {
                        Longitude = coord[0],
                        Latitude = coord[1]
                    });
                }
            }
        }

        return osrmRoute;
    }
}

// Clases de deserialización del API OSRM
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
    public Geometry Geometry { get; set; }
}

public class Geometry
{
    [JsonPropertyName("coordinates")]
    public List<List<double>> Coordinates { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
}
