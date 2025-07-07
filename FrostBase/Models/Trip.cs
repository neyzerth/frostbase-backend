using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

public class Trip
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<Trip> _tripColl = 
        MongoDbConnection.GetCollection<Trip>("Trips");
    
    #endregion

    #region properties

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("date")]
    public DateTime Date { get; set; }

    [BsonElement("start_hour")]
    public TimeSpan StartHour { get; set; }

    [BsonElement("end_hour")]   
    public TimeSpan? EndHour { get; set; }

    [BsonElement("IDStateTrip")]  
    public string IDStateTrip { get; set; }

    [BsonElement("total_time")] 
    public TimeSpan? TotalTime { get; set; }
    
    [BsonElement("id_route")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IDRoute { get; set; }

    [BsonElement("orders")]
    public List<TripOrder> Orders { get; set; }

    #endregion
    
    #region class methods

    public static List<Trip> Get() 
    {
        //Test
        List<Trip> trips =
        [
            new Trip
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Date = DateTime.Now,
                StartHour = new TimeSpan(8, 0, 0),
                EndHour = null,
                IDStateTrip = "START",
                TotalTime = null,
                IDRoute = ObjectId.GenerateNewId().ToString(),
                Orders = null
            },
            new Trip
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Date = DateTime.Now.AddDays(1),
                StartHour = new TimeSpan(9, 0, 0),
                EndHour = new TimeSpan(14, 0, 0),
                IDStateTrip = "ENDED",
                TotalTime = new TimeSpan(5, 0, 0),
                IDRoute = ObjectId.GenerateNewId().ToString(),
                Orders = null
            },
        ];
        
        return trips;
    }

    public static Trip Get(string id)
    {
        //Test
        Trip t = new Trip
        {
            Id = id,
            Date = DateTime.Now,
            StartHour = new TimeSpan(8, 0, 0),
            EndHour = new TimeSpan(12, 0, 0),
            TotalTime = new TimeSpan(4, 0, 0),
            IDRoute = ObjectId.GenerateNewId().ToString(),
            Orders = new List<TripOrder>()
        };
        //End test
        
        // Para implementación real con MongoDB:
        // var filter = Builders<Trip>.Filter.Eq(t => t.Id, id);
        // return _tripColl.Find(filter).FirstOrDefault();
        
        return t;
    }

    public static Trip Insert(Trip t)
    {
        try
        {
            // Si no hay ID, generar uno nuevo
            if (string.IsNullOrEmpty(t.Id))
            {
                t.Id = ObjectId.GenerateNewId().ToString();
            }
            
            // Asegurarse de que Orders esté inicializado
            if (t.Orders == null)
            {
                t.Orders = new List<TripOrder>();
            }
            
            _tripColl.InsertOne(t);
            return t; // Devuelve el objeto con el ID asignado
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
    public static Trip Insert(CreateTripDto c)
    {
        Trip t = new Trip
        {
            Id = c.Id, // Puede ser null o vacío, el método Insert principal se encargará de generar un nuevo ID
            Date = c.Date,
            StartHour = c.StartHour,
            EndHour = c.EndHour,
            IDStateTrip = c.State,
        };
        
        return Insert(t);
    }
    public static Trip Insert(StartTripDto c)
    {
        Trip t = new Trip
        {
            Date = c.Date,
            StartHour = c.StartHour,
            IDRoute = c.IDRoute,
            IDStateTrip = c.State,
            Orders = new List<TripOrder>()
        };
        
        t.Orders.Add(new TripOrder
        {
            IDOrder = ObjectId.GenerateNewId().ToString(),
            IDStore = ObjectId.GenerateNewId().ToString(),
            TimeStart = DateTime.Now,
            TimeEnd = null
        });
        return Insert(t);
    }
    
    public static bool UpdateEndTime(string tripId, TimeSpan? endHour, string stateId)
    {
        try
        {
            TimeSpan totalTime = endHour.Value.Subtract(Trip.Get(tripId).StartHour);
            var filter = Builders<Trip>.Filter.Eq(t => t.Id, tripId);
            var update = Builders<Trip>.Update
                .Set(t => t.EndHour, endHour)
                .Set(t => t.IDStateTrip, stateId)
                .Set(t => t.TotalTime, totalTime);
                
            var result = _tripColl.UpdateOne(filter, update);
            return result.ModifiedCount > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
    public static bool StartOrder(string tripId, string orderId, string storeId = null)
    {
        try
        {
            // Crear un nuevo objeto TripOrder
            TripOrder newOrder = new TripOrder
            {
                IDOrder = orderId,
                IDStore = storeId,
                TimeStart = DateTime.Now,
                TimeEnd = null
            };
            
            // Crear un filtro para encontrar el viaje por su ID
            var filter = Builders<Trip>.Filter.Eq(t => t.Id, tripId);
            
            try
            {
                // Primero intentar añadir directamente, asumiendo que Orders ya existe como array
                var pushUpdate = Builders<Trip>.Update.Push(t => t.Orders, newOrder);
                var result = _tripColl.UpdateOne(filter, pushUpdate);
                
                if (result.ModifiedCount > 0)
                {
                    return true; // Éxito en el primer intento
                }
            }
            catch (MongoWriteException ex) when (ex.Message.Contains("must be an array but is of type null"))
            {
                // Si falla porque Orders es null, inicializarlo con el nuevo elemento
                var setUpdate = Builders<Trip>.Update.Set(t => t.Orders, new List<TripOrder> { newOrder });
                var result = _tripColl.UpdateOne(filter, setUpdate);
                
                return result.ModifiedCount > 0;
            }
            
            // Si llegamos aquí, el documento no se encontró o no se modificó
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
    
    public static bool EndOrder(string tripId, string orderId)
    {
        try
        {
            // Verificar si el viaje existe y tiene órdenes
            var trip = _tripColl.Find(t => t.Id == tripId).FirstOrDefault();
            
            if (trip == null)
            {
                return false; // El viaje no existe
            }
            
            if (trip.Orders == null || !trip.Orders.Any(o => o.IDOrder == orderId))
            {
                return false; // No hay órdenes o la orden específica no existe
            }
            
            // Crear un filtro que encuentre el viaje y la orden específica dentro de ese viaje
            var filter = Builders<Trip>.Filter.And(
                Builders<Trip>.Filter.Eq(t => t.Id, tripId),
                Builders<Trip>.Filter.ElemMatch(t => t.Orders, o => o.IDOrder == orderId)
            );
            
            // Crear una actualización que establezca el tiempo de fin de la orden
            var update = Builders<Trip>.Update
                .Set("orders.$.time_end", DateTime.Now);
                
            var result = _tripColl.UpdateOne(filter, update);
            return result.ModifiedCount > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
    

    #endregion
}
