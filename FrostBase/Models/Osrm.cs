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
                //Console.WriteLine("||| Ruta encontrada en la base de datos |||");
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

        var url = $"https://router.project-osrm.org/route/v1/driving/{startLon},{startLat};{endLon},{endLat}?overview=simplified&geometries=geojson";
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
    
    public static Location MoveTowardsMeters(Location A, Location B, double stepMeters)
{
    const double EarthRadius = 6371000; // Radio de la Tierra en metros

    double latA = DegreeToRadian(A.Latitude);
    double lonA = DegreeToRadian(A.Longitude);
    double latB = DegreeToRadian(B.Latitude);
    double lonB = DegreeToRadian(B.Longitude);

    // Calcular la distancia entre A y B (Haversine)
    double distance = HaversineDistance(latA, lonA, latB, lonB);

    if (stepMeters >= distance || distance == 0)
    {
        return new Location { Latitude = B.Latitude, Longitude = B.Longitude };
    }

    // Calcular el bearing
    double bearing = CalculateBearingRadians(latA, lonA, latB, lonB);

    // Fracción de la distancia
    double fraction = stepMeters / distance;

    // Fórmulas para encontrar el nuevo punto
    double newLat = Math.Asin(Math.Sin(latA) * Math.Cos(fraction * distance / EarthRadius) +
                              Math.Cos(latA) * Math.Sin(fraction * distance / EarthRadius) * Math.Cos(bearing));

    double newLon = lonA + Math.Atan2(Math.Sin(bearing) * Math.Sin(fraction * distance / EarthRadius) * Math.Cos(latA),
                                      Math.Cos(fraction * distance / EarthRadius) - Math.Sin(latA) * Math.Sin(newLat));

    return new Location
    {
        Latitude = RadianToDegree(newLat),
        Longitude = RadianToDegree(newLon)
    };
}

    private static double DegreeToRadian(double degree) => degree * Math.PI / 180.0;
    private static double RadianToDegree(double radian) => radian * 180.0 / Math.PI;

    private static double HaversineDistance(double lat1, double lon1, double lat2, double lon2)
    {
        const double EarthRadius = 6371000; // Radio de la Tierra en metros

        double dLat = lat2 - lat1;
        double dLon = lon2 - lon1;

        double a = Math.Pow(Math.Sin(dLat / 2), 2) +
                   Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(dLon / 2), 2);

        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return EarthRadius * c;
    }

    public static double HaversineDistance(Location A, Location B)
    {
        return HaversineDistance(A.Latitude, A.Longitude, B.Latitude, B.Longitude);;
    }

    private static double CalculateBearingRadians(double latA, double lonA, double latB, double lonB)
    {
        double dLon = lonB - lonA;
        double y = Math.Sin(dLon) * Math.Cos(latB);
        double x = Math.Cos(latA) * Math.Sin(latB) -
                   Math.Sin(latA) * Math.Cos(latB) * Math.Cos(dLon);

        return Math.Atan2(y, x);
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
public class Location
{
    public double Latitude { get; set; }
    
    public double Longitude { get; set; }
    public Location() { }
    
    public Location(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
    public Location(StoreLocation location)
    {
        Latitude = location.Latitude;
        Longitude = location.Longitude;
    }
}