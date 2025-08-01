using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class RouteController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        List<RouteDto> routes = RouteDto.FromModel(Route.Get());
        return Ok(ListResponse<RouteDto>.GetResponse(routes));
    }
    [HttpGet("{id}")]
    public ActionResult Get(string id)
    {
        RouteDto route = RouteDto.FromModel(Route.Get(id));
        return Ok(Response<RouteDto>.GetResponse(route));
    }
    // [HttpPost]
    // public ActionResult Post([FromForm] Route r)
    // {
    //     if(Route.Insert(r))
    //         return Ok(MessageResponse.GetResponse(1, "Road inserted", MessageType.Success));
    //     
    //     return BadRequest(MessageResponse.GetResponse(1, "Road not inserted", MessageType.Error));
    // }
    // [HttpPut("{id}")]
    // public ActionResult Put(int id /*, [FromPost] PostRoad p (??)*/)
    // {
    //     return Ok(MessageResponse.GetResponse(1, "Road "+ id +" updated", MessageType.Success));
    // }
    // [HttpDelete("{id}")]
    // public ActionResult Delete(int id)
    // {
    //     return Ok(MessageResponse.GetResponse(1, "Road "+ id +" deleted", MessageType.Success));
    // }
    
    [HttpGet("today/")]
    public ActionResult GetTodayRoutes()
    {
        
        List<RouteDto> route = RouteDto.FromModel(Route.GetByDate(DateTime.Now));
        return Ok(ListResponse<RouteDto>.GetResponse(route));
    }
    [HttpGet("days/{day}")]
    public ActionResult GetTodayRoutes(int day)
    {
        List<RouteDto> route = RouteDto.FromModel(Route.GetByDay(day));
        return Ok(ListResponse<RouteDto>.GetResponse(route));
    }
    
    [HttpGet("pending/{routeId}&{date}")]
    public ActionResult GetPending(string routeId, DateTime? date = null)
    {
        date??=DateTime.Now;
        List<OrderDto> orders = OrderDto.FromModel(Route.PendingOrdersOrders(routeId, date.Value));
        return Ok(ListResponse<OrderDto>.GetResponse(orders));
    }
}