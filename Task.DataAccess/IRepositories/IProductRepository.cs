using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Core.Entities;

namespace Task.DataAccess.IRepositories
{
    public interface IProductRepository:IBaseRepository<Product>
    {
        Task<bool> Update(Product product);
    }
}
