using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class StoreController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        List<Store> stores = Store.Get();
        return Ok(StoreListView.GetResponse(stores, 1));
    }
    [HttpGet("{id}")]
    public ActionResult Get(int id)
    {
        Store store = Store.Get(id);
        return Ok(StoreView.GetResponse(store, 1));
    }
    [HttpPost]
    public ActionResult Post([FromForm] Store s)
    {
        if(Store.Insert(s))
            return Ok(MessageResponse.GetResponse(1, "Store inserted", MessageType.Success));
        
        return BadRequest(MessageResponse.GetResponse(1, "Store not inserted", MessageType.Error));
    }
    [HttpPut("{id}")]
    public ActionResult Put(int id /*, [FromPost] PostStore p (??)*/)
    {
        return Ok(MessageResponse.GetResponse(1, "Store "+ id +" updated", MessageType.Success));
    }
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        return Ok(MessageResponse.GetResponse(1, "Store "+ id +" deleted", MessageType.Success));
    }
}