using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ParameterController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        List<Parameter> parameters = Parameter.Get();
        return Ok(ListResponse<Parameter>.GetResponse(parameters, 1));
    }
    // [HttpGet("{id}")]
    // public ActionResult Get(int id)
    // {
    //     Parameter parameter = Parameter.Get(id);
    //     return Ok(Response<Parameter>.GetResponse(parameter, 1));
    // }
    // [HttpPost]
    // public ActionResult Post()
    // {
    //     return Ok(MessageResponse.GetResponse(1, "Parameter inserted", MessageType.Success));
    // }
    // [HttpPut("{id}")]
    // public ActionResult Put(int id)
    // {
    //     return Ok(MessageResponse.GetResponse(1, "Parameter "+ id +" updated", MessageType.Success));
    // }
    // [HttpDelete("{id}")]
    // public ActionResult Delete(int id)
    // {
    //     return Ok(MessageResponse.GetResponse(1, "Parameter "+ id +" deleted", MessageType.Success));
    // }
}
