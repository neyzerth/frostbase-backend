using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ParameterController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        ParameterDto parameter = ParameterDto.FromModel(Parameter.Get()); //error(?
        return Ok(Response<ParameterDto>.GetResponse(parameter));
    }

    [HttpPut]
    public ActionResult Put([FromBody] UpdateParameterDto p)
    {
        Parameter updatedParam = Parameter.Update(p);

        if (updatedParam != null)
            return Ok(Response<ParameterDto>.GetResponse(ParameterDto.FromModel(updatedParam)));
        return BadRequest(MessageResponse.GetResponse("Error updating parameters. Parameters were not updated", 1,
            MessageType.Error));

    }
}