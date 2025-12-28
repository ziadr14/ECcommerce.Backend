//using ECom.BLL.DTOs;
using ECom.BLL.DTOs;
using ECom.DAL.Entities.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECom.BLL.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseDto?> CreateOrderAsync(CreateOrderDto dto);
        Task<IReadOnlyList<OrderResponseDto>> GetAllOrdersAsync();

        Task<IReadOnlyList<OrderResponseDto>> GetOrdersByEmailAsync();
        Task<OrderResponseDto?> GetOrderByIdAsync(int id, string? email = null);
        Task<bool> UpdateOrderStatusAsync(int id, Status status);
        Task<bool> DeleteOrderAsync(int id);
    }
}
