using AutoMapper;
using ECom.BLL.DTOs;
using ECom.BLL.Interfaces;
using ECom.DAL.Entities.Order;
using ECom.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECom.BLL.Services
{
    public class OrderService : IOrderService
    {

    private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerBasketSercvice _basketService;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEmailService _emailService;


        public OrderService(
            IUnitOfWork unitOfWork,
            ICustomerBasketSercvice basketService,
            IMapper mapper,
            ICurrentUserService currentUserService,
            IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _basketService = basketService;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _emailService = emailService;

        }

        public async Task<OrderResponseDto?> CreateOrderAsync(CreateOrderDto dto)
        {
            var basket = await _basketService.GetBasketAsync(dto.BasketId);
            if (basket == null || !basket.Items.Any())
                return null;

            var orderItems = _mapper.Map<List<OrderItem>>(basket.Items);

            var deliveryMethod =
                await _unitOfWork.DeliveryMethods.GetById(dto.DeliveryMethodId);

            if (deliveryMethod == null)
                return null;

            var shippingAddress =
                _mapper.Map<ShippingAddress>(dto.ShippingAddress);

            var buyerEmail = _currentUserService.BuyerEmail;
            shippingAddress.BuyerEmail = buyerEmail;

            var order = new Orders
            {
                BuyerEmail = buyerEmail,
                ShippingAddress = shippingAddress,
                DeliveryMethod = deliveryMethod,
                OrderItems = orderItems,
                SubTotal = orderItems.Sum(i => i.Price * i.Quantity),
                Status = Status.PaymentPending,
                OrderDate = DateTime.UtcNow
            };

            await _unitOfWork.Orders.AddAsync(order);

            return _mapper.Map<OrderResponseDto>(order);
        }



        public async Task<IReadOnlyList<OrderResponseDto>> GetAllOrdersAsync()
        {
            var orders = await _unitOfWork.Orders
                .GetAllQueryable()
                .Include(o => o.OrderItems)
                .Include(o => o.DeliveryMethod)
                .Include(o => o.ShippingAddress)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return _mapper.Map<IReadOnlyList<OrderResponseDto>>(orders);
        }
        public async Task<IReadOnlyList<OrderResponseDto>> GetOrdersByEmailAsync()
        {
            var email = _currentUserService.BuyerEmail;

            if (string.IsNullOrEmpty(email))
                return new List<OrderResponseDto>();

            var orders = await _unitOfWork.Orders
                .GetAllQueryable()
                .Where(o => o.BuyerEmail == email)
                .Include(o => o.OrderItems)
                .Include(o => o.DeliveryMethod)
                .Include(o => o.ShippingAddress)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return _mapper.Map<IReadOnlyList<OrderResponseDto>>(orders);
        }

        public async Task<OrderResponseDto?> GetOrderByIdAsync(int id, string? email = null)
        {
            var query = _unitOfWork.Orders
                .GetAllQueryable()
                .Include(o => o.OrderItems)
                .Include(o => o.DeliveryMethod)
                .Include(o => o.ShippingAddress)
                .Where(o => o.Id == id);

            if (!string.IsNullOrEmpty(email))
                query = query.Where(o => o.BuyerEmail == email);

            var order = await query.FirstOrDefaultAsync();
            return order is null ? null : _mapper.Map<OrderResponseDto>(order);
        }
        public async Task<bool> UpdateOrderStatusAsync(int id, Status status)
        {
            var order = await _unitOfWork.Orders.GetById(id);
            if (order == null) return false;

            order.Status = status;
            await _unitOfWork.Orders.UpdateAsync(order);

            return true;
        }


        public async Task<bool> DeleteOrderAsync(int id)
        {
            await _unitOfWork.Orders.DeleteAsync(id);
            return true;
        }

    }
}
