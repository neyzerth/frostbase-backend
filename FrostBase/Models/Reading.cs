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

    public Reading()
    {
        Id = ObjectId.GenerateNewId().ToString();
        Latitude = 90;
        Longitude = 180;
        DoorState = false;
    }

    #region class methods

    public static List<Reading> Get() 
    {
        return _readingColl.Find(r => r.Date <= DateTime.Now).ToList();
    }

    public static Reading Get(string id)
    {
        return _readingColl.Find(r => r.Id == id).FirstOrDefault();
    }
    public static List<Reading> GetByTruck(string truckId)
    {
        return _readingColl.Find(r => r.IDTruck == truckId && r.Date <= DateTime.Now).ToList();
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
            Console.WriteLine("Reading insert: "+e);
            throw new Exception("Error inserting reading: "+e.Message);
        }
    }
    public static List<Reading> Insert(List<Reading> readings)
    {
        try
        {
            foreach (var r in readings)
            {
                if(string.IsNullOrEmpty(r.Id))
                    r.Id = ObjectId.GenerateNewId().ToString();
            }
            _readingColl.InsertMany(readings);
            return readings;
        }
        catch (Exception e)
        {
            Console.WriteLine("Reading insert: "+e);
            throw new Exception("Error inserting many reading: "+e.Message);
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

    #region simulation

    public static List<Reading> TripReadings(Trip trip)
    {
        var readings = new List<Reading>();
        var date = trip.StartTime;
        var truck = trip.IDTruck;
        var doorState = false;
        var temp = 0.0;
        var humidity = 0;
        var lat = 0.0;
        var lon = 0.0;
        return null;

    }

    public static List<Reading> OrderReadings(TripOrder tripOrder)
    {
        var rnd = new Random();
        var readings = new List<Reading>();
        var date = tripOrder.StartTime;
        var parameter = Parameter.Get();
        
        var temperature = (double)(
                              parameter.MaxTemperature + 
                              parameter.MinTemperature
                              )/2 + rnd.NextDouble();
        var humidity = (double)(
                              parameter.MaxHumidity + 
                              parameter.MinHumidity
                              )/2 + rnd.NextDouble();
        var location = new Location
        {
            Latitude = 32.45900929216648,
            Longitude = -116.97966765227373
        };
        
        var reading = new Reading
        {
            DoorState = true,
            Temperature = temperature,
            PercHumidity = (int)humidity,
            Longitude = location.Longitude,
            Latitude = location.Latitude,
            Date = date
        };
        
        readings.Add(reading);
        while (date <= tripOrder.EndTime)
        {
            date = date.AddMinutes(5);
            temperature =+ rnd.NextDouble() - 0.5;
            humidity =+ rnd.NextDouble() - 2;
            
            reading = new Reading
            {
                DoorState = true,
                Temperature = temperature,
                PercHumidity = (int)humidity,
                Longitude = location.Longitude,
                Latitude = location.Latitude,
                Date = date
            };
            readings.Add(reading);
        }

        return null;
    }

    public static Reading BaseReading(DateTime start, string truckId)
    {
        var location = new Location
        {
            Latitude = 32.45900929216648,
            Longitude = -116.97966765227373
        };

        return BaseReading(location, start, truckId);
    }
    
    public static Reading BaseReading(Location location, DateTime start, string truckId)
    {
        var rnd = new Random();
        var parameter = Parameter.Get();
        var temperature = (double)(
                              parameter.MaxTemperature + 
                              parameter.MinTemperature
                              )/2 + rnd.NextDouble();
        var humidity = (double)(
                              parameter.MaxHumidity + 
                              parameter.MinHumidity
                              )/2 + rnd.NextDouble();
        
        var reading = new Reading
        {
            DoorState = true,
            Temperature = temperature,
            PercHumidity = (int)humidity,
            Longitude = location.Longitude,
            Latitude = location.Latitude,
            IDTruck = truckId,
            Date = start
        };

        return reading;
    }

    public static List<Reading> GenerateTripReadings(Reading baseReading, DateTime start, DateTime end, Location destination, int minutesStep = 5)
    {
        var rnd = new Random();
        var readings = new List<Reading>();
        readings.Add(baseReading);

        var currentLoc = new Location { Latitude = baseReading.Latitude, Longitude = baseReading.Longitude };
        baseReading.DoorState = false;

        var osrmRoute = Osrm.Get(currentLoc, destination);

        if (osrmRoute.Geometry == null || osrmRoute.Geometry.Count < 2)
            throw new Exception("La ruta no tiene suficientes puntos");

        double distancePerStep = (minutesStep * osrmRoute.Distance) / osrmRoute.Duration;

        int geometryIndex = 0;
        Location nextWaypoint = osrmRoute.Geometry[geometryIndex + 1];

        while (start <= end && geometryIndex < osrmRoute.Geometry.Count - 1)
        {
            double remainingDistance = Osrm.HaversineDistance(currentLoc, nextWaypoint);

            // Si el paso es mayor o igual a la distancia al siguiente punto, saltamos al siguiente punto directamente
            if (distancePerStep >= remainingDistance)
            {
                currentLoc = nextWaypoint;
                geometryIndex++;

                if (geometryIndex >= osrmRoute.Geometry.Count - 1)
                    break;

                nextWaypoint = osrmRoute.Geometry[geometryIndex + 1];
            }
            else
            {
                // Avanzar step metros hacia el siguiente punto
                currentLoc = Osrm.MoveTowardsMeters(currentLoc, nextWaypoint, distancePerStep);
            }

            start = start.AddMinutes(minutesStep);

            var reading = new Reading
            {
                Latitude = currentLoc.Latitude,
                Longitude = currentLoc.Longitude,
                Date = start,
                Temperature = baseReading.Temperature + rnd.NextDouble() - 0.5,
                PercHumidity = baseReading.PercHumidity + rnd.Next(-2, 2),
                IDTruck = baseReading.IDTruck,
                DoorState = false
            };
            Console.WriteLine($"-- Trip Reading: Location({reading.Latitude},{reading.Longitude}) | Date({reading.Date}),)");

            readings.Add(reading);
        }

        return readings;
    }

    public static List<Reading> GenerateStaticReading(Reading baseReading, DateTime start, DateTime end, Location point, int minutesStep = 5)
    {
        var rnd = new Random();
        var readings = new List<Reading>();
        while (start <= end)
        {
            start = start.AddMinutes(minutesStep);
            var reading = new Reading
            {
                Latitude = point.Latitude,
                Longitude = point.Longitude,
                Date = start,
                Temperature = baseReading.Temperature + rnd.NextDouble() - 0.5,
                PercHumidity = baseReading.PercHumidity + rnd.Next(-2, 2),
                IDTruck = baseReading.IDTruck,
                DoorState = false
            };
            
            Console.WriteLine($"-- Stayed Reading: Location({reading.Latitude},{reading.Longitude}) | Date({reading.Date}),)");
            readings.Add(reading);
        }

        return readings;
    }


    #endregion
}
