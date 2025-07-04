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
            State = "START"
        };
        
        Trip trip = Trip.Insert(t);
        t.Id = trip.Id;
        
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
    [HttpPost("Start/Order/{idOrder}")]
    public ActionResult StartOrder(string idOrder)
    {
        if(true) 
            return Ok(MessageResponse.GetResponse(1, "Started order " + idOrder, MessageType.Success));
            
        return BadRequest(MessageResponse.GetResponse(0, "Failed to end trip", MessageType.Error));
    }
}