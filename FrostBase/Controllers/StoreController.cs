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
        return Ok(ListResponse<StoreDto>.GetResponse(stores));
    }
    [HttpGet("{id}")]
    public ActionResult Get(string id)
    {
        StoreDto store = StoreDto.FromModel(Store.Get(id));
        return Ok(Response<StoreDto>.GetResponse(store));
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
    
    
    [HttpGet("no-ordered/")]
    public ActionResult GetNoOrdered()
    {
        List<StoreDto> stores = StoreDto.FromModel(Store.GetNotOrders());
        return Ok(ListResponse<StoreDto>.GetResponse(stores));
    }
    [HttpGet("ordered/{id}")]
    public ActionResult GetOrdered(string id)
    {
        if(Store.Ordered(id))
            return Ok(MessageResponse.GetResponse( "Store "+ id +" has ordered"));
        
        return BadRequest(MessageResponse.GetResponse( "Store "+ id +" hasn't ordered", 1, MessageType.Error));
    }
}