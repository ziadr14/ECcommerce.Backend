using ECom.BLL.DTOs;
using ECom.BLL.Interfaces;
using ECom.BLL.Services;
using ECom.DAL.Entities.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECom.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IInvoiceService _invoiceService;

        public OrdersController(IOrderService orderService , IInvoiceService invoiceService)
        {
            _orderService = orderService;
            _invoiceService = invoiceService;
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
        {
            var order = await _orderService.CreateOrderAsync(dto);
            if (order == null)
                return BadRequest("Order creation failed.");

            return Ok(order);
        }
 
        [HttpGet("my-orders")]
        public async Task<IActionResult> GetMyOrders()
        {
            return Ok(await _orderService.GetOrdersByEmailAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _orderService.GetAllOrdersAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id, [FromQuery] string? email = null)
        {
            var order = await _orderService.GetOrderByIdAsync(id, email);

            if (order == null)
                return NotFound("Order not found");

            return Ok(order);
        }


        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(
            int id,
            [FromQuery] Status status)
        {
            var updated = await _orderService.UpdateOrderStatusAsync(id, status);

            if (!updated)
                return NotFound("Order not found");

            return Ok("Order status updated successfully");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return NoContent(); 
        }
        [HttpPost("{id}/send-invoice")]
        public async Task<IActionResult> SendInvoice(int id)
        {
            var result = await _invoiceService.SendInvoicePdfAsync(id);

            if (!result)
                return BadRequest(new { message = "Failed to send invoice" });

            return Ok(new { message = "Invoice sent successfully" });
        }


    }
}
