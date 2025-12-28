using ECom.BLL.DTOs;
using ECom.DAL.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.BLL.Interfaces
{
    public interface IDeliveryMethodService
    {
        Task<IReadOnlyList<DeliveryMethodDto>> GetAllAsync();
        Task<DeliveryMethodDto?> GetByIdAsync(int id);

        Task<DeliveryMethodDto> CreateAsync(CreateDeliveryMethodDto dto);
        Task<bool> UpdateAsync(int id, UpdateDeliveryMethodDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
