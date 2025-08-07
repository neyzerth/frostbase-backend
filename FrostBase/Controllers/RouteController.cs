using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class RouteController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        var routes = RouteMapDto.FromModel(Route.Get());
        return Ok(ListResponse<RouteMapDto>.GetResponse(routes));
    }
    [HttpGet("{id}")]
    public ActionResult Get(string id)
    {
        var route = RouteMapDto.FromModel(Route.Get(id));
        return Ok(Response<RouteMapDto>.GetResponse(route));
    }
    [HttpGet("driver/{driverId}")]
    public ActionResult GetByDriver(string driverId)
    {
        var route = RouteMapDto.FromModel(Route.GetByDriver(driverId));
        return Ok(Response<RouteMapDto>.GetResponse(route));
    }
    [HttpPost]
    public ActionResult Post([FromBody] CreateRouteDto r)
    {
        var route = Route.Insert(r);
        return Ok(Response<RouteDto>.GetResponse(RouteDto.FromModel(route)));
        
    }
    [HttpPut]
    public ActionResult Put([FromBody] UpdateRouteDto r)
    {
        var route = Route.Update(r);
        return Ok(Response<RouteDto>.GetResponse(RouteDto.FromModel(route)));
    }
    [HttpDelete("{id}")]
    public ActionResult Delete(string id)
    {
        var route = Route.Delete(id);
        return Ok(Response<RouteDto>.GetResponse(RouteDto.FromModel(route)));
    }
    
    [HttpGet("today/")]
    public ActionResult GetTodayRoutes()
    {
        
        var route = RouteMapDto.FromModel(Route.GetByDate(DateTime.Now));
        return Ok(ListResponse<RouteMapDto>.GetResponse(route));
    }
    [HttpGet("days/{day}")]
    public ActionResult GetTodayRoutes(int day)
    {
        var route = RouteMapDto.FromModel(Route.GetByDay(day));
        return Ok(ListResponse<RouteMapDto>.GetResponse(route));
    }
    
    [HttpGet("pending/{routeId}&{date}")]
    public ActionResult GetPending(string routeId, DateTime? date = null)
    {
        date??=DateTime.Now;
        List<OrderDto> orders = OrderDto.FromModel(Route.PendingOrdersOrders(routeId, date.Value));
        return Ok(ListResponse<OrderDto>.GetResponse(orders));
    }
}