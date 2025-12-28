using ECom.DAL.Entities;
using ECom.DAL.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepositories<Product> Products { get; }
        IBaseRepositories<Category> Categories { get; }
        IBaseRepositories<Photo> Photos { get; }


        IBaseRepositories<CustomBasket> CustomBaskets { get; }
        IBaseRepositories<Orders> Orders { get; }
        IBaseRepositories<DeliveryMethod> DeliveryMethods { get; }

        Task<int> CompleteAsync();

    }
}
