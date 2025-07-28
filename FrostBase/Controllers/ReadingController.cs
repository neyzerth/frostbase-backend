using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ReadingController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        List<Reading> readings = Reading.Get();
        return Ok(ListResponse<Reading>.GetResponse(readings));
    }

    [HttpGet("{id}")]
    public ActionResult Get(string id)
    {
        Reading reading = Reading.Get(id);
        return Ok(Response<Reading>.GetResponse(reading));
    }

    [HttpPost("Truck/{idTruck}")]
    public ActionResult Post(string idTruck, [FromBody] CreateReadingDto c)
    {   
        
        return Ok(MessageResponse.GetResponse("Reading inserted"));
    }

    // [HttpPut("{id}")]
    // public ActionResult Put(int id /*, [FromForm] UpdateReadingDto u*/)
    // {
    //     return Ok(MessageResponse.GetResponse(1, $"Reading {id} updated", MessageType.Success));
    // }
    //
    // [HttpDelete("{id}")]
    // public ActionResult Delete(int id)
    // {
    //     return Ok(MessageResponse.GetResponse(1, $"Reading {id} deleted", MessageType.Success));
    // }
}