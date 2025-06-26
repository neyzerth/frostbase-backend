using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class StoreController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        return Ok(MessageResponse.GetResponse(1, "Stores list", MessageType.Success));
    }
    [HttpGet("{id}")]
    public ActionResult Get(int id)
    {
        return Ok(MessageResponse.GetResponse(1, "Info of Store "+ id, MessageType.Success));
    }
    [HttpPost]
    public ActionResult Post(/*[FromPost] PostStore p*/)
    {
        return Ok(MessageResponse.GetResponse(1, "Store inserted", MessageType.Success));
    }
    [HttpPut("{id}")]
    public ActionResult Put(int id /*, [FromPost] PostStore p (??)*/)
    {
        return Ok(MessageResponse.GetResponse(1, "Store "+ id +" updated", MessageType.Success));
    }
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        return Ok(MessageResponse.GetResponse(1, "Store "+ id +" deleted", MessageType.Success));
    }
}