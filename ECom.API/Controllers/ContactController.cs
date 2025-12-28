using ECom.BLL.DTOs;
using ECom.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/contact")]
public class ContactController : ControllerBase
{
    private readonly IEmailService _emailService;

    public ContactController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost]
    public async Task<IActionResult> Send(ContactMessageDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _emailService.SendEmailAsync(dto);
        return Ok(new { message = "Message sent successfully" });
    }
}
