using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ReadingController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        List<ReadingDto> readings = ReadingDto.FromModel(Reading.Get());
        return Ok(ListResponse<ReadingDto>.GetResponse(readings));
    }

    [HttpGet("{id}")]
    public ActionResult Get(string id)
    {
        ReadingDto reading = ReadingDto.FromModel(Reading.Get(id));
        return Ok(Response<ReadingDto>.GetResponse(reading));
    }
    [HttpGet("Truck")]
    public ActionResult GetByTruck()
    {
        var readings = ReadingsByTruckDto.FromModel(Reading.GetByTruck());
        return Ok(ListResponse<ReadingsByTruckDto>.GetResponse(readings));
    }
    [HttpGet("Truck/{truckId}")]
    public ActionResult GetByTruck(string truckId)
    {
        var reading = ReadingDto.FromModel(Reading.GetByTruck(truckId));
        return Ok(ListResponse<ReadingDto>.GetResponse(reading));
    }
    
    [HttpGet("Truck/Latest/{idTruck}")]
    public ActionResult GetLatestByTrcuk(string idTruck)
    {
        var reading = Reading.GetLatestByTruck(idTruck);
    
        if (reading == null)
            return NotFound($"No reading found for truck with ID {idTruck}");

        var dto = ReadingDto.FromModel(reading);
        return Ok(dto);
    }
    [HttpGet("Truck/Latest")]
    public ActionResult GetLatestByTrcuk()
    {
        var reading = Reading.GetLatestByTruck();

        var dto = ReadingByTruckDto.FromModel(reading);
        return Ok(dto);
    }

    [HttpPost("Truck/{idTruck}")]
    public ActionResult Post(string idTruck, [FromBody] CreateReadingDto c)
    {   
        ReadingDto reading = ReadingDto.FromModel(Reading.Insert(idTruck, c));
        return Ok(Response<ReadingDto>.GetResponse(reading));
    }

    // [HttpPut]
    // public ActionResult Put([FromBody] UpdateReadingDto r)
    // {
    //     return Ok(MessageResponse.GetResponse();
    // }
    //
    // [HttpDelete("{id}")]
    // public ActionResult Delete(int id)
    // {
    //     return Ok(MessageResponse.GetResponse(1, $"Reading {id} deleted", MessageType.Success));
    // }
}