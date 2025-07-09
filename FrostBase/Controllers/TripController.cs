using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class TripController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        List<TripDto> trips = TripDto.FromModel(Trip.Get());
        return Ok(ListResponse<TripDto>.GetResponse(trips, 1));
    }
    [HttpGet("{id}")]
    public ActionResult Get(string id)
    {
        TripDto trip = TripDto.FromModel(Trip.Get(id));
        return Ok(Response<TripDto>.GetResponse(trip, 1));
    }
    [HttpPost]
    public ActionResult Post([FromBody] CreateTripDto c)
    {
        Trip insertedTrip = Trip.Insert(c);
        if(insertedTrip != null) 
            return Ok(Response<Trip>.GetResponse(insertedTrip, 1));
            
        return BadRequest(MessageResponse.GetResponse(0, "Trip not inserted", MessageType.Error));
    }
    // [HttpPut("{id}")]
    // public ActionResult Put(int id, [FromForm] CreateUserDto t)
    // {
    //     return Ok(MessageResponse.GetResponse(1, "Trip "+ id +" updated", MessageType.Success));
    // }
    // [HttpDelete("{id}")]
    // public ActionResult Delete(int id)
    // {
    //     return Ok(MessageResponse.GetResponse(1, "Trip "+ id +" deleted", MessageType.Success));
    // }
    
    
    [HttpPost("[action]")]
    public ActionResult Start([FromBody] StartTripDto t)
    {
        TripDto trip = TripDto.FromModel(Trip.Insert(t));
        
        if(trip != null) 
            return Ok(Response<TripDto>.GetResponse(1, trip, MessageType.Success));
            
        return BadRequest(MessageResponse.GetResponse(0, "Trip not inserted", MessageType.Error));
    }
    [HttpPost("[action]/{idTrip}/")]
    public ActionResult End(string idTrip)
    {
        TripDto trip = TripDto.FromModel(Trip.UpdateEndTime(idTrip));
        if(trip != null) 
            return Ok(Response<TripDto>.GetResponse(1, trip, MessageType.Success));
            
        return BadRequest(MessageResponse.GetResponse(0, "Failed to end trip", MessageType.Error));
    }
    
    [HttpPost("{tripId}/[action]/{orderId}")]
    public ActionResult StartOrder(string tripId, string orderId)
    {
        TripDto trip = TripDto.FromModel(Trip.StartOrder(tripId, orderId));
        if(trip != null)
            return Ok(Response<TripDto>.GetResponse(1, trip, MessageType.Success));
            
        return BadRequest(MessageResponse.GetResponse(0, "Failed to start order", MessageType.Error));
    }
    
    [HttpPost("{tripId}/order/{orderId}/end")]
    public ActionResult EndOrder(string tripId, string orderId)
    {
        TripDto trip = TripDto.FromModel(Trip.EndOrder(tripId, orderId));
        if(trip != null)
            return Ok(Response<TripDto>.GetResponse(1, trip, MessageType.Success));
            
        return BadRequest(MessageResponse.GetResponse(0, "Failed to end order", MessageType.Error));
    }
}