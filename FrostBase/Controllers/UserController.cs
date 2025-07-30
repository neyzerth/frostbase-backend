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
        List<DriverDto> drivers = DriverDto.FromModel(UserApp.GetDriver());
        return Ok(ListResponse<DriverDto>.GetResponse(drivers));
    }
    [HttpGet("Admins")]
    public ActionResult GetAdmins()
    {
        List<AdminDto> admins = AdminDto.FromModel(UserApp.GetAdmin());
        return Ok(ListResponse<AdminDto>.GetResponse(admins));
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
    
    [HttpPost("login")]
    public ActionResult Login([FromBody] LoginUserDto log)
    {
        UserDto user = UserDto.FromModel(UserApp.Login(log.Email, log.Password));
        
        return Ok(Response<UserDto>.GetResponse(user));
    }
}