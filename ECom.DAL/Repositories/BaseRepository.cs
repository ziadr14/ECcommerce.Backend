using ECom.DAL.Data;
using ECom.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECom.DAL.Repositories
{
    public class BaseRepository<T> : IBaseRepositories<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }


        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public IQueryable<T> GetAllQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public Task<IQueryable<T>> GetByIdQueryable(int id)
        {
            var query = _dbSet.Where(e => EF.Property<int>(e, "Id") == id).AsQueryable();
            return Task.FromResult(query);
        }


        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
                throw new Exception("Entity not found");

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
