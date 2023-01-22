using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicisPOS.Application.Services.Abstractions;
using PublicisPOS.Domain.Entities;
using PublicisPOS.DTO;

namespace PublicisPOS.Controllers
{
    public class DealController : ControllerBase
    {
        private readonly IDealService _dealService;

        public DealController(IDealService dealService)
        {
            _dealService = dealService;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddDeal([FromBody]DealDto dealDto)
        {
            if (dealDto == null)
                return BadRequest("Deal Input is not valid");
            DealDto saved = await _dealService.CreateDeal(dealDto);
            return CreatedAtAction(nameof(GetDeal),new {Id=saved.Id},saved.Id);
        }

        [HttpPost]
        [Route("remove")]
        public async Task<IActionResult> RemoveDeal(int dealId)
        {
            await _dealService.DeleteDeal(dealId);
            return Ok();
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetDeals()
        {
            var deals = await _dealService.GetAllDeals();
            return Ok(deals);
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetDeal(int id)
        {
            var deal = await _dealService.GetDealById(id);
            return Ok(deal);
        }
    }

}
