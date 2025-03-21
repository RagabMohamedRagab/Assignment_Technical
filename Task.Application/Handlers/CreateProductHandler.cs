using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Task.Application.Commands;
using Task.Core.Entities;
using Task.DataAccess.IRepositories;

namespace Task.Application.Handlers
{
    public class CreateProductHandler(IBaseRepository<Product> productRepository, IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : IRequestHandler<CreateProductCommand, bool>
    {
        private readonly IBaseRepository<Product> _productRepository = productRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor = httpContextAccessor;
        public async Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var userId = _contextAccessor.HttpContext.Request.Cookies["UserId"] ?? _contextAccessor.HttpContext?.User?.FindFirst("Id")?.Value;
            var product = _mapper.Map<Product>(request);
            product.TotalPrice = product.Price * product.Quantity;
            product.UserId = userId;
            if (await _productRepository.AddAsync(product))
            {
                return await _unitOfWork.CommitAsync() > 0;
            }
            return false;
        }
    }
}
