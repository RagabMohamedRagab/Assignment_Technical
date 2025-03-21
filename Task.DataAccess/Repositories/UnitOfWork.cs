using Task.DataAccess.Context;
using Task.DataAccess.IRepositories;

namespace Task.DataAccess.Repositories
{
    public class UnitOfWork(TaskDbContext Context) : IUnitOfWork
    {
        private readonly TaskDbContext _Context = Context;
        public async Task<int> CommitAsync()
        {
          return await _Context.SaveChangesAsync();
        }

        public void Dispose() => _Context.Dispose();
    }
}
