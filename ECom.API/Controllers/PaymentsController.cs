using ECom.BLL.Interfaces;
using ECom.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {

        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomBasket>> CreateOrUpdatePayment(
            string basketId,
            [FromQuery] int? deliveryMethodId)
        {
            var basket = await _paymentService
                .CreateOrUpdatePayment(basketId, deliveryMethodId);

            if (basket == null)
                return BadRequest("Basket not found");

            return Ok(basket);
        }

        [HttpPost("checkout")]
        public async Task<ActionResult<string>> CreateCheckout(
            string basketId,
            int deliveryMethodId)
        {
            var url = await _paymentService
                .CreateStripeCheckoutSession(basketId, deliveryMethodId);

            return Ok(url);
        }
    }

}


