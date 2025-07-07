using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AlertController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        List<Alert> alerts = Alert.Get();
        return Ok(ListResponse<Alert>.GetResponse(alerts, 1));
    }
    [HttpGet("{id}")]
    public ActionResult Get(int id)
    {
        Alert alert = Alert.Get(id);
        return Ok(Response<Alert>.GetResponse(alert, 1));
    }
    // [HttpPost]
    // public ActionResult Post(/*[FromPost] PostAlert p*/)
    // {
    //     return Ok(MessageResponse.GetResponse(1, "Alert inserted", MessageType.Success));
    // }
    // [HttpPut("{id}")]
    // public ActionResult Put(int id /*, [FromPost] PostAlert p (??)*/)
    // {
    //     return Ok(MessageResponse.GetResponse(1, "Alert "+ id +" updated", MessageType.Success));
    // }
    // [HttpDelete("{id}")]
    // public ActionResult Delete(int id)
    // {
    //     return Ok(MessageResponse.GetResponse(1, "Alert "+ id +" deleted", MessageType.Success));
    // }
}