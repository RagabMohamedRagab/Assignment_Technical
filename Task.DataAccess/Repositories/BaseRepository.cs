
using Microsoft.EntityFrameworkCore;
using Task.DataAccess.Context;
using Task.DataAccess.IRepositories;

namespace Task.DataAccess.Repositories
{
    public class BaseRepository<T>(TaskDbContext Context) : IBaseRepository<T> where T : class
    {
        private readonly TaskDbContext _Context = Context;
        private DbSet<T> _dbSet = Context.Set<T>();

        public async Task<bool> AddAsync(T entity)
        {
            try
            {
                _dbSet.Add(entity);
                return true;
            }
            catch (Exception ex) { return false; }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<T> GetAsync(Guid Id) => await _dbSet.FindAsync(Id);
    }
}
