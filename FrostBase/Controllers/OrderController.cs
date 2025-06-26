using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        return Ok(MessageResponse.GetResponse(1, "Orders list", MessageType.Success));
    }
    [HttpGet("{id}")]
    public ActionResult Get(int id)
    {
        return Ok(MessageResponse.GetResponse(1, "Info of Order "+ id, MessageType.Success));
    }
    [HttpPost]
    public ActionResult Post(/*[FromPost] PostOrder p*/)
    {
        return Ok(MessageResponse.GetResponse(1, "Order inserted", MessageType.Success));
    }
    [HttpPut("{id}")]
    public ActionResult Put(int id /*, [FromPost] PostOrder p (??)*/)
    {
        return Ok(MessageResponse.GetResponse(1, "Order "+ id +" updated", MessageType.Success));
    }
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        return Ok(MessageResponse.GetResponse(1, "Order "+ id +" deleted", MessageType.Success));
    }
}