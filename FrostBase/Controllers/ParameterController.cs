using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ParameterController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        ParameterDto parameter = ParameterDto.FromModel(Parameter.Get());
        return Ok(Response<ParameterDto>.GetResponse(parameter));
    }
    
    // [HttpPost]
    // public ActionResult Post([FromBody] CreateParameterDto c)
    // { 
    //     Parameter insert = Parameter.Insert(c);
    //     
    //     if(insert != null) 
    //         return Ok(Response<ParameterDto>.GetResponse(ParameterDto.FromModel(insert)));
    //     return BadRequest(MessageResponse.GetResponse("Order not inserted", 1, MessageType.Error));
    // }
    
    
    
    }
    
