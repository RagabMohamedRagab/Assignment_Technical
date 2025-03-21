using System.Runtime.CompilerServices;

namespace Task.DataAccess.IRepositories
{
    public interface IBaseRepository<T> where T : class
    {
         Task<IEnumerable<T>> GetAllAsync();
         Task<T> GetAsync(Guid Id);
         Task<bool> AddAsync(T entity);

    }
}
