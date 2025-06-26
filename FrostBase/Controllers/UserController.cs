using Microsoft.AspNetCore.Mvc;
using FrostBase.Models.User;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        List<User> users = FrostBase.Models.User.User.Get();
        return Ok(UserListView.GetResponse(users, 1));
    }
    [HttpGet("{id}")]
    public ActionResult Get(int id)
    {
        User user = FrostBase.Models.User.User.Get(id);
        return Ok(MessageResponse.GetResponse(1, user, MessageType.Success));
    }
    [HttpPost]
    public ActionResult Post(/*[FromPost] PostUser p*/)
    {
        return Ok(MessageResponse.GetResponse(1, "User inserted", MessageType.Success));
    }
    [HttpPut("{id}")]
    public ActionResult Put(int id /*, [FromPost] PostUser p (??)*/)
    {
        return Ok(MessageResponse.GetResponse(1, "User "+ id +" updated", MessageType.Success));
    }
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        return Ok(MessageResponse.GetResponse(1, "User "+ id +" deleted", MessageType.Success));
    }
}