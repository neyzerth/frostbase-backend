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
        
        List<RouteDto> route = RouteDto.FromModel(Route.GetByDate(DateTime.Now));
        return Ok(ListResponse<RouteDto>.GetResponse(route));
    }
    [HttpGet("days/{day}")]
    public ActionResult GetTodayRoutes(int day)
    {
        List<RouteDto> route = RouteDto.FromModel(Route.GetByDay(day));
        return Ok(ListResponse<RouteDto>.GetResponse(route));
    }
}