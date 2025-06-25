using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        return Ok("List of Users");
    }
    [HttpGet("{id}")]
    public ActionResult Get(int id)
    {
        return Ok("Info of user " + id);
    }
    [HttpPost]
    public ActionResult Post(/*[FromPost] PostUser p*/)
    {
        return Ok("User inserted successfully");
    }
    [HttpPut("{id}")]
    public ActionResult Put(int id /*, [FromPost] PostUser p (??)*/)
    {
        return Ok("User " + id + " updated successfully");
    }
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        return Ok("User " + id + " deleted successfully");
    }
}