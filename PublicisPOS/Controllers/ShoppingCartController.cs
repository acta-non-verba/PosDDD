using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicisPOS.Application.Services;
using PublicisPOS.Domain.Entities;

namespace PublicisPOS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _cartService;

        public ShoppingCartController(IShoppingCartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        [Route("AddItem")]
        public async Task<IActionResult> AddItem([FromBody] CartItem item)
        {
            var result = await _cartService.Save(item);
            if (result.Success)
                return Ok(result.Value);
            else
                return BadRequest(result.Error);
        }

        [HttpPost]
        [Route("RemoveItem")]
        public async Task<IActionResult> RemoveItem([FromBody] CartItem item)
        {
            var result = await _cartService.RemoveItem(item);
            if (result.Success)
                return Ok(result.Value);
            else
                return BadRequest(result.Error);
        }

        [HttpGet]
        [Route("GetCart")]
        public async Task<IActionResult> GetCart()
        {
            var result = await _cartService.GetCart();
            if (result.Success)
                return Ok(result.Value);
            else
                return BadRequest(result.Error);
        }
    }

}
