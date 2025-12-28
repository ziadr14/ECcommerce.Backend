using ECom.BLL.Interfaces;
using ECom.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BasketController : ControllerBase
{
    private readonly ICustomerBasketSercvice _basketService;

    public BasketController(ICustomerBasketSercvice basketService)
    {
        _basketService = basketService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomBasket>> GetBasket(string id)
    {
        var basket = await _basketService.GetBasketAsync(id);
        return basket ?? new CustomBasket(id);
    }

    [HttpPost]
    public async Task<ActionResult<CustomBasket>> UpdateBasket(CustomBasket basket)
    {
        var updatedBasket = await _basketService.UpdateBasketAsync(basket);
        return Ok(updatedBasket);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBasket(string id)
    {
        await _basketService.DeleteBasketAsync(id);
        return NoContent();
    }
}
