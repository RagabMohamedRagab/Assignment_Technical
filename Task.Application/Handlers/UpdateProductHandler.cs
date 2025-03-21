using AutoMapper;
using MediatR;
using Task.Application.Commands;
using Task.Core.Entities;
using Task.DataAccess.IRepositories;

namespace Task.Application.Handlers
{
    public class UpdateProductHandler(IBaseRepository<Product> productRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IBaseRepository<Product> _productRepository = productRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productRepository.GetAsync(request.Id);
                if (product is null) return false;

                product.Quantity = request.Quantity;
                product.TotalPrice = request.Quantity * product.Price;
                product.Date = request.Date;

                return await _unitOfWork.CommitAsync() > 0;
            }
            catch (Exception ex) { return false; }


        }
    }
}
