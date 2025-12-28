using ECom.BLL.DTOs;
using ECom.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DeliveryMethodsController : ControllerBase
{
    private readonly IDeliveryMethodService _service;

    public DeliveryMethodsController(IDeliveryMethodService service)
    {
        _service = service;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var method = await _service.GetByIdAsync(id);
        if (method == null) return NotFound();

        return Ok(method);
    }


    //[Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateDeliveryMethodDto dto)
    {
        var method = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = method.Id }, method);
    }


    //[Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateDeliveryMethodDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        if (!updated) return NotFound();

        return NoContent();
    }

    //[Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();

        return NoContent();
    }
}
