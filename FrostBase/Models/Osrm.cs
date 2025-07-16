using System.Text.Json;

public class Osrm
{
    #region properties

    private static readonly HttpClient _httpClient; 

    public double Distance { get; set; }
    public double Duration { get; set; }

    public Osrm() { }

    #endregion

    #region class methods

    public static async Task<Osrm> GetRoute(Location start, Location end)
    {
        return await GetRoute(start.Latitude, start.Longitude, end.Latitude, end.Longitude);
    }

    public static async Task<Osrm> GetRoute(double startLat, double startLon, double endLat, double endLon)
    {
        var url = $"https://router.project-osrm.org/route/v1/driving/{startLon},{startLat};{endLon},{endLat}?overview=false";

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        
        // Deserialize into the OSRM API response model
        var osrmApiResponse = JsonSerializer.Deserialize<OsrmApiResponse>(json);

        var route = osrmApiResponse?.Routes?.FirstOrDefault();

        if (route == null)
            throw new Exception("No se encontro una ruta valida");

        return new Osrm
        {
            Distance = route.Distance ,
            Duration = route.Duration
        };
    }

    #endregion
}

// Define classes that match the OSRM API response structure
public class OsrmApiResponse
{
    public string Code { get; set; }
    public List<OsrmRoute> Routes { get; set; }
}

public class OsrmRoute
{
    public double Distance { get; set; } 
    public double Duration { get; set; } 
    public string Geometry { get; set; }
}