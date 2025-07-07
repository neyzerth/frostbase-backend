using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class TruckController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        List<Truck> trucks = Truck.Get();
        return Ok(ListResponse<Truck>.GetResponse(trucks, 1));
    }
    [HttpGet("{id}")]
    public ActionResult Get(int id)
    {
        return Ok(Response<Truck>.GetResponse(Truck.Get(id), 1));
    }

    // [HttpPost]
    // public ActionResult Post([FromForm] Truck t)
    // {
    //     if(Truck.Insert(t))
    //         return Ok(MessageResponse.GetResponse(1, "Truck inserted", MessageType.Success));
    //     
    //     return BadRequest(MessageResponse.GetResponse(0, "Truck not inserted", MessageType.Error));
    // }
    // [HttpPut("{id}")]
    // public ActionResult Put(int id)
    // {
    //     return Ok(MessageResponse.GetResponse(1, "Truck "+ id +" updated", MessageType.Success));
    // }
    // [HttpDelete("{id}")]
    // public ActionResult Delete(int id)
    // {
    //     return Ok(MessageResponse.GetResponse(1, "Truck "+ id +" deleted", MessageType.Success));
    // }
}