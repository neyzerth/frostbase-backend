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
    [HttpPost]
    public ActionResult Post([FromBody] CreateStoreDto s)
    {
        StoreDto store = StoreDto.FromModel(Store.Insert(s)); 
        return Ok(Response<StoreDto>.GetResponse(store));
        
    }
    [HttpPut]
    public ActionResult Put([FromBody] UpdateStoreDto s)
    {
        StoreDto store = StoreDto.FromModel(Store.Update(s)); 
        return Ok(Response<StoreDto>.GetResponse(store));
    }
    [HttpDelete("{id}")]
    public ActionResult Delete(string id)
    {
        
        StoreDto store = StoreDto.FromModel(Store.Delete(id)); 
        return Ok(Response<StoreDto>.GetResponse(store));
    }
    
    
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