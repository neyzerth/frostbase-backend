using Microsoft.AspNetCore.Mvc;

[Route("api/simulator/")]
[ApiController]
public class SimulatorController : ControllerBase
{
    [HttpPost("order/generate")]
    public ActionResult GenerateOrder([FromBody] Simulate simulate)
    {
        OrderDto order = OrderDto.FromModel(Order.GenerateOrder(simulate.Date));
        return Ok(Response<OrderDto>.GetResponse( order));
    }
    
    [HttpPost("trip/generate")]
    public ActionResult GenerateTrip([FromBody] Simulate simulate)
    {
        TripDto trip = TripDto.FromModel(Trip.Simulate(simulate.Date));
        return Ok(Response<TripDto>.GetResponse( trip));
    }
    
    [HttpPost("trip/check/")]
    public ActionResult CheckSimulatedTrips([FromBody] Simulate simulate)
    {
        List<TripDto> sim = TripDto.FromModel(SimulationTrip.CheckSimulationsTrips(simulate.Date)); 
        return Ok(ListResponse<TripDto>.GetResponse( sim));
    }
    
    [HttpPost("reading/generate")]
    public ActionResult GenerateReading()
    {
        return Ok("not implemented");
    }
    [HttpPost("error")]
    public ActionResult GenerateError()
    {
        throw new FrostbaseException("test", 1, 404);
    }
    
}