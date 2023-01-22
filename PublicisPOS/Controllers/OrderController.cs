using Microsoft.AspNetCore.Mvc;
using PublicisPOS.Application.Services.Abstractions;
using PublicisPOS.Domain.Aggregates;

namespace PublicisPOS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] Order order)
        {
            var result = await _orderService.Save(order);
            if (result.Id>0)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }

}
