using ECom.DAL.Data;
using ECom.DAL.Entities;
using ECom.DAL.Entities.Order;
using ECom.DAL.Interfaces;
using ECom.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IBaseRepositories<Product> Products { get; private set; }
        public IBaseRepositories<Category> Categories { get; private set; }
        public IBaseRepositories<Photo> Photos { get; private set; }

        //public IBaseRepositories<ProductType> ProductTypes { get; private set; }
        public IBaseRepositories<CustomBasket> CustomBaskets { get; private set; }
        public IBaseRepositories<Orders> Orders { get; private set; }
        public IBaseRepositories<DeliveryMethod> DeliveryMethods { get; private set; }


        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Products = new BaseRepository<Product>(_context);
            Categories = new BaseRepository<Category>(_context);
            Photos = new BaseRepository<Photo>(_context);
            //ProductTypes = new BaseRepository<ProductType>(_context);
            CustomBaskets = new BaseRepository<CustomBasket>(_context);
            Orders = new BaseRepository<Orders>(_context);
            DeliveryMethods = new BaseRepository<DeliveryMethod>(_context);  
        }


        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }


    }
}
