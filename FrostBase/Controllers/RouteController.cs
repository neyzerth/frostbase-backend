using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class RoadController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        List<Route> routes = Route.Get();
        return Ok(RouteListView.GetResponse(routes, 1));
    }
    [HttpGet("{id}")]
    public ActionResult Get(int id)
    {
        Route route = Route.Get(id);
        return Ok(RouteView.GetResponse(route, 1));
    }
    [HttpPost]
    public ActionResult Post([FromForm] Route r)
    {
        if(Route.Insert(r))
            return Ok(MessageResponse.GetResponse(1, "Road inserted", MessageType.Success));
        
        return BadRequest(MessageResponse.GetResponse(1, "Road not inserted", MessageType.Error));
    }
    [HttpPut("{id}")]
    public ActionResult Put(int id /*, [FromPost] PostRoad p (??)*/)
    {
        return Ok(MessageResponse.GetResponse(1, "Road "+ id +" updated", MessageType.Success));
    }
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        return Ok(MessageResponse.GetResponse(1, "Road "+ id +" deleted", MessageType.Success));
    }
}