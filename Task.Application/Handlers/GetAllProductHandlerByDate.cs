using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Task.Application.Queries;
using Task.Application.ViewModel;
using Task.Core.Entities;
using Task.DataAccess.IRepositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Task.Application.Handlers
{
    public class GetAllProductHandlerByDate(IBaseRepository<Product> productRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetAllProductQueryByDate, GetAllProductViewModel>
    {
        private readonly IBaseRepository<Product> _productRepository = productRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _contextAccessor = httpContextAccessor;

        public async Task<GetAllProductViewModel> Handle(GetAllProductQueryByDate request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _contextAccessor.HttpContext.Request.Cookies["UserId"] ?? _contextAccessor.HttpContext?.User?.FindFirst("Id")?.Value;
                var Products = await _productRepository.GetAllAsync();
                List<GetAllProductViewModel> Filterproducts = new();
                if (!Products.Any() || userId is null)
                    return new GetAllProductViewModel { TotalRecord = 0, datas = new List<Data>() };

                Products = Products.Where(b => !b.IsDeleted && b.UserId == userId);
                var totalRecord = Products.Count();
                if (request.From.HasValue)
                {
                    Products = Products.Where(b => b.Date.HasValue && b.Date.Value.Date >= request.From.Value.Date);
                }

                if (request.To.HasValue)
                {
                    Products = Products.Where(b => b.Date.HasValue && b.Date.Value.Date <= request.To.Value.Date);
                }
                var data = _mapper.Map<List<Data>>(Products);

                return new GetAllProductViewModel { TotalRecord = totalRecord, datas = data };
            }
            catch (Exception ex) { return null; }
        }
    }
}
