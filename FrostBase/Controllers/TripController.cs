using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class TripController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        return Ok(MessageResponse.GetResponse(1, "Trips list", MessageType.Success));
    }
    [HttpGet("{id}")]
    public ActionResult Get(int id)
    {
        return Ok(MessageResponse.GetResponse(1, "Info of Trip "+ id, MessageType.Success));
    }
    [HttpPost]
    public ActionResult Post(/*[FromPost] PostTrip p*/)
    {
        return Ok(MessageResponse.GetResponse(1, "Trip inserted", MessageType.Success));
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