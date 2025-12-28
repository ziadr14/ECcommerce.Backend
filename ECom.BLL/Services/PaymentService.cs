using ECom.BLL.Interfaces;
using ECom.DAL.Entities;
using ECom.DAL.Entities.Order;
using ECom.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;

namespace ECom.BLL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ICustomerBasketSercvice _customerBasket;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;             
        private readonly ICurrentUserService _currentUser;
        public PaymentService(
            ICustomerBasketSercvice customerBasket,
            IConfiguration configuration      ,  IUnitOfWork unitOfWork,
        ICurrentUserService currentUser)
        {
            _customerBasket = customerBasket;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<CustomBasket> CreateOrUpdatePayment(
            string basketId,
            int? deliveryMethodId)
        {
            var basket = await _customerBasket.GetBasketAsync(basketId);
            if (basket == null) return null;

            StripeConfiguration.ApiKey =
                _configuration["StripeSetting:Secretkey"];

            decimal shippingPrice = 0m;

            basket.DeliveryMethodId = deliveryMethodId;
            basket.ShippingPrice = shippingPrice;

            var amount = basket.Items.Sum(i => i.Price * i.Quantity)
                         + shippingPrice;

            var paymentIntentService = new PaymentIntentService();
            PaymentIntent paymentIntent;

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(amount * 100), 
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                paymentIntent = await paymentIntentService.CreateAsync(options);

                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)(amount * 100)
                };

                await paymentIntentService.UpdateAsync(
                    basket.PaymentIntentId,
                    options);
            }

            await _customerBasket.UpdateBasketAsync(basket);

            return basket;
        }
        public async Task<string> CreateStripeCheckoutSession(
            string basketId,
            int deliveryMethodId)
        {
            var basket = await _customerBasket.GetBasketAsync(basketId);
            if (basket == null)
                throw new Exception("Basket not found");

            var buyerEmail = _currentUser.BuyerEmail;

            var order = await _unitOfWork.Orders
                .GetAllQueryable()
                .OrderByDescending(o => o.Id)
                .FirstOrDefaultAsync(o =>
                    o.BuyerEmail == buyerEmail &&
                    o.Status == Status.PaymentPending);

            if (order == null)
                throw new Exception("No pending order found");

            StripeConfiguration.ApiKey =
                _configuration["StripeSetting:Secretkey"];

            var lineItems = basket.Items.Select(item =>
                new SessionLineItemOptions
                {
                    Quantity = item.Quantity,
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        UnitAmount = (long)(item.Price * 100),
                        ProductData =
                            new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.ProductName
                            }
                    }
                }).ToList();

            var shippingPrice = order.ShippingPrice;

            var options = new SessionCreateOptions
            {
                Mode = "payment",
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,

                ShippingOptions = new List<SessionShippingOptionOptions>
        {
            new SessionShippingOptionOptions
            {
                ShippingRateData =
                    new SessionShippingOptionShippingRateDataOptions
                    {
                        Type = "fixed_amount",
                        FixedAmount =
                            new SessionShippingOptionShippingRateDataFixedAmountOptions
                            {
                                Amount = (long)(shippingPrice * 100),
                                Currency = "usd"
                            },
                        DisplayName = "Standard Shipping"
                    }
            }
        },

                SuccessUrl =
                    "http://localhost:4200/orders/success?session_id={CHECKOUT_SESSION_ID}",

                CancelUrl =
                    "http://localhost:4200/checkout",

                Metadata = new Dictionary<string, string>
        {
            { "orderId", order.Id.ToString() },
            { "basketId", basketId }
        }
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            order.StripeSessionId = session.Id;
            order.PaymentIntentId = session.PaymentIntentId;
            await _unitOfWork.Orders.UpdateAsync(order);

            return session.Url!;
        }


    }
}
