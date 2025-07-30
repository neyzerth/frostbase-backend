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
        return Ok(ListResponse<Alert>.GetResponse(alerts));
    }
    [HttpGet("{id}")]
    public ActionResult Get(string id)
    {
        Alert alert = Alert.Get(id);
        return Ok(Response<Alert>.GetResponse(alert));
    }
    
    [HttpPost]
    public ActionResult Post([FromBody] CreateAlertDto a)
    {
        Alert insert = Alert.Insert(a);
        if(insert != null)
            return Ok(Response<AlertDto>.GetResponse(AlertDto.FromModel(insert)));
        
        return BadRequest(MessageResponse.GetResponse("Alert not inserted", 1, MessageType.Error));
    }
  
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