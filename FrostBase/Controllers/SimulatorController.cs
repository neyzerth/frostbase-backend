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
    
    [HttpPost("trip/generate")]
    public ActionResult GenerateTrip()
    {
        TripDto trip = TripDto.FromModel(Trip.Simulate());
        return Ok(Response<TripDto>.GetResponse( trip, 1));
    }
    
    [HttpPost("reading/generate")]
    public ActionResult GenerateReading()
    {
        return Ok("not implemented");
    }
    
}