using Microsoft.AspNetCore.Mvc;

[Route("simulator/")]
[ApiController]
public class SimulatorController : ControllerBase
{
    [HttpPost("order/generate")]
    public ActionResult GenerateOrder()
    {
        OrderDto order = OrderDto.FromModel(Order.GenerateOrder());
        return Ok(Response<OrderDto>.GetResponse( order, 1));
    }
    
    [HttpPost("trip/start/generate")]
    public ActionResult GenerateTrip()
    {
        return Ok("not implemented");
    }
    
    [HttpPost("reading/generate")]
    public ActionResult GenerateReading()
    {
        return Ok("not implemented");
    }
    
}