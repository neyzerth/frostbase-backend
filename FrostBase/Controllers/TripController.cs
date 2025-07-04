using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class TripController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        List<Trip> trips = Trip.Get();
        return Ok(ListResponse<Trip>.GetResponse(trips, 1));
    }
    [HttpGet("{id}")]
    public ActionResult Get(string id)
    {
        Trip trip = Trip.Get(id);
        return Ok(Response<Trip>.GetResponse(trip, 1));
    }
    [HttpPost]
    public ActionResult Post([FromForm] CreateTripDto c)
    {
        Trip insertedTrip = Trip.Insert(c);
        if(insertedTrip != null) 
            return Ok(Response<Trip>.GetResponse(insertedTrip, 1));
            
        return BadRequest(MessageResponse.GetResponse(0, "Trip not inserted", MessageType.Error));
    }
    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromForm] CreateUserDto t)
    {
        return Ok(MessageResponse.GetResponse(1, "Trip "+ id +" updated", MessageType.Success));
    }
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        return Ok(MessageResponse.GetResponse(1, "Trip "+ id +" deleted", MessageType.Success));
    }
    
    
    [HttpPost("[action]/route/{idRoute}/")]
    public ActionResult Start(string idRoute)
    {
        StartTripDto t = new StartTripDto
        {
            Date = DateTime.Now,
            StartHour = DateTime.Now.TimeOfDay,
            IDRoute = idRoute,
            State = "START",
            Orders = new List<TripOrder>() // Inicializar Orders como una lista vacía
        };
        
        Trip trip = Trip.Insert(t);
        t.Id = trip.Id;
        t.Orders = trip.Orders;
        
        if(trip != null) 
            return Ok(Response<StartTripDto>.GetResponse(1, t, MessageType.Success));
            
        return BadRequest(MessageResponse.GetResponse(0, "Trip not inserted", MessageType.Error));
    }
    [HttpPost("[action]/{idTrip}/")]
    public ActionResult End(string idTrip)
    {
        Trip t = new Trip
        {
            Id = idTrip,
            EndHour = DateTime.Now.TimeOfDay,
            IDStateTrip = "ENDED"
        };
        if(Trip.UpdateEndTime(t.Id, t.EndHour, t.IDStateTrip)) 
            return Ok(MessageResponse.GetResponse(1, "Trip ended successfully", MessageType.Success));
            
        return BadRequest(MessageResponse.GetResponse(0, "Failed to end trip", MessageType.Error));
    }
    
    [HttpPost("{tripId}/order/{orderId}/start")]
    public ActionResult StartOrder(string tripId, string orderId, [FromQuery] string storeId = null)
    {
        if(Trip.StartOrder(tripId, orderId, storeId))
            return Ok(MessageResponse.GetResponse(1, "Order started successfully", MessageType.Success));
            
        return BadRequest(MessageResponse.GetResponse(0, "Failed to start order", MessageType.Error));
    }
    
    [HttpPost("{tripId}/order/{orderId}/end")]
    public ActionResult EndOrder(string tripId, string orderId)
    {
        if(Trip.EndOrder(tripId, orderId))
            return Ok(MessageResponse.GetResponse(1, "Order ended successfully", MessageType.Success));
            
        return BadRequest(MessageResponse.GetResponse(0, "Failed to end order", MessageType.Error));
    }
}