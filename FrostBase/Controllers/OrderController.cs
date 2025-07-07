using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        List<Order> orders = Order.Get();
        return Ok(ListResponse<Order>.GetResponse(orders, 1));
    }
    [HttpGet("{id}")]
    public ActionResult Get(string id)
    {
        Order order = Order.Get(id);
        return Ok(Response<Order>.GetResponse(order, 1));
    }
    [HttpPost]
    public ActionResult Post([FromForm] CreateOrderDto c)
    {
        Order insert = Order.Insert(c);
        if(insert != null)
            return Ok(Response<Order>.GetResponse(insert, 1));
        
        return BadRequest(MessageResponse.GetResponse(0, "Order not inserted", MessageType.Error));
    }
    // [HttpPut("{id}")]
    // public ActionResult Put(int id /*, [FromPost] PostOrder p (??)*/)
    // {
    //     return Ok(MessageResponse.GetResponse(1, "Order "+ id +" updated", MessageType.Success));
    // }
    // [HttpDelete("{id}")]
    // public ActionResult Delete(int id)
    // {
    //     return Ok(MessageResponse.GetResponse(1, "Order "+ id +" deleted", MessageType.Success));
    // }
}