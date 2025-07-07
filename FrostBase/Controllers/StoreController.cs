using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class StoreController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        List<StoreDto> stores = StoreDto.FromModel(Store.Get());
        return Ok(ListResponse<StoreDto>.GetResponse(stores, 1));
    }
    [HttpGet("{id}")]
    public ActionResult Get(string id)
    {
        StoreDto store = StoreDto.FromModel(Store.Get(id));
        return Ok(Response<StoreDto>.GetResponse(store, 1));
    }
    // [HttpPost]
    // public ActionResult Post([FromForm] Store s)
    // {
    //     if(Store.Insert(s))
    //         return Ok(MessageResponse.GetResponse(1, "Store inserted", MessageType.Success));
    //     
    //     return BadRequest(MessageResponse.GetResponse(1, "Store not inserted", MessageType.Error));
    // }
    // [HttpPut("{id}")]
    // public ActionResult Put(int id /*, [FromPost] PostStore p (??)*/)
    // {
    //     return Ok(MessageResponse.GetResponse(1, "Store "+ id +" updated", MessageType.Success));
    // }
    // [HttpDelete("{id}")]
    // public ActionResult Delete(int id)
    // {
    //     return Ok(MessageResponse.GetResponse(1, "Store "+ id +" deleted", MessageType.Success));
    // }
}