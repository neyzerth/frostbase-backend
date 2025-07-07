using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        List<UserApp> users = UserApp.Get();
        return Ok(ListResponse<UserApp>.GetResponse(users, 1));
    }
    [HttpGet("{id}")]
    public ActionResult Get(int id)
    {
        UserApp userApp = UserApp.Get(id);
        return Ok(Response<UserApp>.GetResponse(userApp, 1));
    }
    [HttpPost]
    public ActionResult Post([FromForm] CreateUserDto c)
    {
        if(UserApp.Insert(c))
            return Ok(MessageResponse.GetResponse(1, "User inserted", MessageType.Success));
            
        return BadRequest(MessageResponse.GetResponse(1, "User not inserted", MessageType.Error));
    }
    // [HttpPut("{id}")]
    // public ActionResult Put(int id /*, [FromPost] PostUser p (??)*/)
    // {
    //     return Ok(MessageResponse.GetResponse(1, "User "+ id +" updated", MessageType.Success));
    // }
    // [HttpDelete("{id}")]
    // public ActionResult Delete(int id)
    // {
    //     return Ok(MessageResponse.GetResponse(1, "User "+ id +" deleted", MessageType.Success));
    // }
}