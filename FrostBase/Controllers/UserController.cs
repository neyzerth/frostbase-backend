using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        List<UserDto> users = UserDto.FromModel(UserApp.Get());
        return Ok(ListResponse<UserDto>.GetResponse(users));
    }
    [HttpGet("Drivers")]
    public ActionResult GetDrivers()
    {
        List<UserDto> users = UserDto.FromModel(UserApp.GetDriver());
        return Ok(ListResponse<UserDto>.GetResponse(users));
    }
    [HttpGet("Admins")]
    public ActionResult GetAdmins()
    {
        List<UserDto> users = UserDto.FromModel(UserApp.GetAdmin());
        return Ok(ListResponse<UserDto>.GetResponse(users));
    }
    [HttpGet("{id}")]
    public ActionResult Get(string id)
    {
        UserDto userApp = UserDto.FromModel(UserApp.Get(id));
        return Ok(Response<UserDto>.GetResponse(userApp));
    }
    [HttpPost]
    public ActionResult Post([FromBody] CreateUserDto c)
    {
        UserDto user = UserDto.FromModel(UserApp.Insert(c));
        return Ok(Response<UserDto>.GetResponse(user));
    }
    [HttpPut]
    public ActionResult Put([FromBody] UpdateUserDto c)
    {
        UserDto user = UserDto.FromModel(UserApp.Update(c));
        return Ok(Response<UserDto>.GetResponse(user));
    }
    [HttpDelete("{id}")]
    public ActionResult Delete(string id)
    {
        UserDto user = UserDto.FromModel(UserApp.Delete(id));
        return Ok(Response<UserDto>.GetResponse(user));
    }
}