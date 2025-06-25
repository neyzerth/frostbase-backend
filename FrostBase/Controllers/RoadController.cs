using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class RoadController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        return Ok(MessageResponse.GetResponse(1, "Roads list", MessageType.Success));
    }
    [HttpGet("{id}")]
    public ActionResult Get(int id)
    {
        return Ok(MessageResponse.GetResponse(1, "Info of Road "+ id, MessageType.Success));
    }
    [HttpPost]
    public ActionResult Post(/*[FromPost] PostRoad p*/)
    {
        return Ok(MessageResponse.GetResponse(1, "Road inserted", MessageType.Success));
    }
    [HttpPut("{id}")]
    public ActionResult Put(int id /*, [FromPost] PostRoad p (??)*/)
    {
        return Ok(MessageResponse.GetResponse(1, "Road "+ id +" updated", MessageType.Success));
    }
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        return Ok(MessageResponse.GetResponse(1, "Road "+ id +" deleted", MessageType.Success));
    }
}