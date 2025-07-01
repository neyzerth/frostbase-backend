using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class TripController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        List<Trip> trips = Trip.Get();
        return Ok(TripListView.GetResponse(trips, 1));
    }
    [HttpGet("{id}")]
    public ActionResult Get(int id)
    {
        Trip trip = Trip.Get(id);
        return Ok(TripView.GetResponse(trip, 1));
    }
    [HttpPost]
    public ActionResult Post([FromForm] Trip t)
    {
        if(Trip.Insert(t)) 
            return Ok(MessageResponse.GetResponse(1, "Trip inserted", MessageType.Success));
            
        return BadRequest(MessageResponse.GetResponse(1, "Trip not inserted", MessageType.Error));
    }
    [HttpPut("{id}")]
    public ActionResult Put(int id /*, [FromPost] PostTrip p (??)*/)
    {
        return Ok(MessageResponse.GetResponse(1, "Trip "+ id +" updated", MessageType.Success));
    }
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        return Ok(MessageResponse.GetResponse(1, "Trip "+ id +" deleted", MessageType.Success));
    }
}