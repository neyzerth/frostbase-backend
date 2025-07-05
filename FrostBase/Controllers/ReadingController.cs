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
    public ActionResult Get(int id)
    {
        Reading reading = Reading.Get(id);
        return Ok(Response<Reading>.GetResponse(reading, 1));
    }

    [HttpPost]
    public ActionResult Post([FromForm] CreateReadingDto c)
    {   
        
        
        if(Reading.Insert(c))
            return Ok(MessageResponse.GetResponse(1, "Reading inserted", MessageType.Success));
        
        return BadRequest(MessageResponse.GetResponse(1, "Reading not inserted", MessageType.Error));
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id /*, [FromForm] UpdateReadingDto u*/)
    {
        return Ok(MessageResponse.GetResponse(1, $"Reading {id} updated", MessageType.Success));
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        return Ok(MessageResponse.GetResponse(1, $"Reading {id} deleted", MessageType.Success));
    }
}