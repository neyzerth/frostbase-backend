using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        List<OrderDto> orders = OrderDto.FromModel(Order.Get());
        return Ok(ListResponse<OrderDto>.GetResponse(orders));
    }
    [HttpGet("{id}")]
    public ActionResult Get(string id)
    {
        OrderDto order = OrderDto.FromModel(Order.Get(id));
        return Ok(Response<OrderDto>.GetResponse(order));
    }
    [HttpPost]
    public ActionResult Post([FromBody] CreateOrderDto c)
    {
        Order insert = Order.Insert(c);
        return Ok(Response<OrderDto>.GetResponse(OrderDto.FromModel(insert)));
    }
    
    [HttpPut]
    public ActionResult Put([FromBody] UpdateOrderDto o)
    {
        if (o == null)
            return BadRequest("null or invalid body request.");

        var updatedOrder = Order.Update(o);
        
        if (updatedOrder == null)
            return NotFound("Order not found.");

        var orderDto = OrderDto.FromModel(updatedOrder);
        return Ok(Response<OrderDto>.GetResponse(orderDto));
    }
    
    [HttpDelete("{id}")]
    public ActionResult Delete(string id)
    {
        var orderDto = OrderDto.FromModel(Order.Delete(id));
        return Ok(Response<OrderDto>.GetResponse(orderDto));
    }

    [HttpGet("route/{idRoute}/")]
    public ActionResult GetByRoute(string idRoute)
    {
        List<OrderDto> orders = OrderDto.FromModel(Order.GetByRoute(idRoute));
        return Ok(ListResponse<OrderDto>.GetResponse(orders));
    }

    [HttpGet("pending/")]
    public ActionResult GetPending()
    {
        List<OrderDto> orders = OrderDto.FromModel(Order.GetPending());
        return Ok(ListResponse<OrderDto>.GetResponse(orders));
    }
    
}
