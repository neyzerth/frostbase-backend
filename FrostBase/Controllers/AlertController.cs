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
    // [HttpPost]
    // public ActionResult Post(/*[FromPost] PostAlert p*/)
    // {
    //     return Ok(MessageResponse.GetResponse(1, "Alert inserted", MessageType.Success));
    // }
    [HttpPut]
    public ActionResult Put([FromBody] UpdateAlertDto a)
    {
        AlertDto alert = AlertDto.FromModel(Alert.Update(a));
        return Ok(Response<AlertDto>.GetResponse(alert));
    }
    // [HttpDelete("{id}")]
    // public ActionResult Delete(int id)
    // {
    //     return Ok(MessageResponse.GetResponse(1, "Alert "+ id +" deleted", MessageType.Success));
    // }
}