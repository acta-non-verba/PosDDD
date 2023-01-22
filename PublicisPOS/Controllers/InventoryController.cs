using Microsoft.AspNetCore.Mvc;
using PublicisPOS.Application.Services.Abstractions;
using PublicisPOS.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _inventoryService;

    public InventoryController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InventoryItem>>> GetInventoryItems()
    {
        var items = await _inventoryService.GetInventoryItemsAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InventoryItem>> GetInventoryItem(int id)
    {
        var item = await _inventoryService.GetInventoryItemAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<InventoryItem>> AddInventoryItem(InventoryItem item)
    {
        var newItem = await _inventoryService.AddInventoryItemAsync(item);
        return CreatedAtAction(nameof(GetInventoryItem), new { id = newItem.Id }, newItem);
    }
}
