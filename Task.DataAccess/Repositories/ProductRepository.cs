using Task.Core.Entities;
using Task.DataAccess.Context;
using Task.DataAccess.IRepositories;

namespace Task.DataAccess.Repositories
{
    public class ProductRepository(TaskDbContext context):BaseRepository<Product>(context), IProductRepository
    {
        public Task<bool> Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
