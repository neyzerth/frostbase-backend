using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        List<UserDto> users = UserDto.FromModel(UserApp.Get());
        return Ok(ListResponse<UserDto>.GetResponse(users, 1));
    }
    [HttpGet("Drivers")]
    public ActionResult GetDrivers()
    {
        List<UserDto> users = UserDto.FromModel(UserApp.GetDriver());
        return Ok(ListResponse<UserDto>.GetResponse(users, 1));
    }
    [HttpGet("Admins")]
    public ActionResult GetAdmins()
    {
        List<UserDto> users = UserDto.FromModel(UserApp.GetAdmin());
        return Ok(ListResponse<UserDto>.GetResponse(users, 1));
    }
    [HttpGet("{id}")]
    public ActionResult Get(string id)
    {
        UserDto userApp = UserDto.FromModel(UserApp.Get(id));
        return Ok(Response<UserDto>.GetResponse(userApp, 1));
    }
    [HttpPost]
    public ActionResult Post([FromBody] CreateUserDto c)
    {
        UserApp inserted = UserApp.Insert(c);
        if(inserted != null )
            return Ok(Response<UserApp>.GetResponse(inserted, 1));
            
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