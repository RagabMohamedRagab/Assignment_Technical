using MediatR;
using Task.Application.Commands;
using Task.Core.Entities;
using Task.DataAccess.IRepositories;

namespace Task.Application.Handlers
{
    public class DeleteProductHandler(IBaseRepository<Product> productRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IBaseRepository<Product> _productRepository = productRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productRepository.GetAsync(request.Id);
                if (product is null) return false;
                product.IsDeleted = true;
                return await _unitOfWork.CommitAsync() > 0;
            }
            catch (Exception)
            {

                return false;
            }

            throw new NotImplementedException();
        }
    }
}
