using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ReadingController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        List<Reading> readings = Reading.Get();
        return Ok(ListResponse<Reading>.GetResponse(readings, 1));
    }

    [HttpGet("{id}")]
    public ActionResult Get(string id)
    {
        Reading reading = Reading.Get(id);
        return Ok(Response<Reading>.GetResponse(reading, 1));
    }

    [HttpPost("Truck/{idTruck}")]
    public ActionResult Post(string idTruck, [FromBody] CreateReadingDto c)
    {   
        
        if(Reading.Insert(idTruck, c))
            return Ok(MessageResponse.GetResponse("Reading inserted", MessageType.Success));
        
        return BadRequest(MessageResponse.GetResponse("Reading not inserted", MessageType.Error, 1));
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