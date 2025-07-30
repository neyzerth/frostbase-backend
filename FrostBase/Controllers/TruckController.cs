using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class TruckController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        List<TruckDto> trucks = TruckDto.FromModel(Truck.Get());
        return Ok(ListResponse<TruckDto>.GetResponse(trucks));
    }
    [HttpGet("{id}")]
    public ActionResult Get(string id)
    {
        TruckDto truck = TruckDto.FromModel(Truck.Get(id));
        return Ok(Response<TruckDto>.GetResponse(truck));
    }

    [HttpPost]
    public ActionResult Post([FromBody] CreateTruckDto t)
    {
        TruckDto truck = TruckDto.FromModel(Truck.Insert(t));
        return Ok(Response<TruckDto>.GetResponse(truck));
    }
    [HttpPut]
    public ActionResult Put([FromBody] UpdateTruckDto t)
    {
        TruckDto truck = TruckDto.FromModel(Truck.Update(t));
        return Ok(Response<TruckDto>.GetResponse(truck));
    }
    [HttpDelete("{id}")]
    public ActionResult Delete(string id)
    {
        TruckDto truck = TruckDto.FromModel(Truck.Delete(id));
        return Ok(Response<TruckDto>.GetResponse(truck));
    }
}