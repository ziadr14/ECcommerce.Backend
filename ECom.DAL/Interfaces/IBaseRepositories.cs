using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECom.DAL.Interfaces
{
    public interface IBaseRepositories<T>  where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> GetAllQueryable();

        Task<T> GetById(int id);

        Task<IQueryable<T>> GetByIdQueryable(int id);

        Task AddAsync(T entity);
        Task UpdateAsync(T entity);

        Task DeleteAsync(int id);
    }
}
