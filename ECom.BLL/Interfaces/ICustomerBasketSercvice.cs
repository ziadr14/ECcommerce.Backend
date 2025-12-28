using ECom.BLL.Services;
using ECom.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.BLL.Interfaces
{
    public interface ICustomerBasketSercvice
    {
        Task<CustomBasket> GetBasketAsync(string basketId);
        Task<CustomBasket?> UpdateBasketAsync(CustomBasket basket);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
