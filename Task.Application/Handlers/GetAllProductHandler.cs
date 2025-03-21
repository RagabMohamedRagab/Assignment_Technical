using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Task.Application.Queries;
using Task.Application.ViewModel;
using Task.Core.Entities;
using Task.DataAccess.IRepositories;

namespace Task.Application.Handlers;

public class GetAllProductHandler(IBaseRepository<Product> productRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetAllProductQuery, GetAllProductViewModel>
{
    private readonly IBaseRepository<Product> _productRepository = productRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IHttpContextAccessor _contextAccessor = httpContextAccessor;

    public async Task<GetAllProductViewModel> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var userId = _contextAccessor.HttpContext.Request.Cookies["UserId"] ?? _contextAccessor.HttpContext?.User?.FindFirst("Id")?.Value;
            var query = (await _productRepository.GetAllAsync()).AsQueryable();

            if (!query.Any() || userId is null)
                return new GetAllProductViewModel { TotalRecord = 0, datas = new List<Data>() };

            query = query.Where(b => !b.IsDeleted && b.UserId == userId);

            if (!string.IsNullOrWhiteSpace(request.SearchValue))
            {
                query = query.Where(b => b.Name.ToLower().Contains(request.SearchValue.ToLower()) || b.Code.ToLower().Contains(request.SearchValue.ToLower()));
            }

            if (request.From.HasValue)
            {
                query = query.Where(b => b.Date.HasValue && b.Date.Value.Date >= request.From.Value.Date);
            }

            if (request.To.HasValue)
            {
                query = query.Where(b => b.Date.HasValue && b.Date.Value.Date <= request.To.Value.Date);
            }

            if (!string.IsNullOrWhiteSpace(request.SortColumn) && !string.IsNullOrWhiteSpace(request.SortDirection))
            {
                bool isAscending = string.Equals(request.SortDirection.Trim(), "asc", StringComparison.OrdinalIgnoreCase);
                query = request.SortColumn.Trim().ToLower() switch
                {
                    "name" => isAscending ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name),
                    "quantity" => isAscending ? query.OrderBy(p => p.Quantity) : query.OrderByDescending(p => p.Quantity),
                    "date" => isAscending ? query.OrderBy(p => p.Date) : query.OrderByDescending(p => p.Date),
                    "unit" => isAscending ? query.OrderBy(p => p.Unit) : query.OrderByDescending(p => p.Unit),
                    "totalprice" => isAscending ? query.OrderBy(p => p.TotalPrice) : query.OrderByDescending(p => p.TotalPrice),
                    _ => query
                };
            }

            var totalRecord = query.Count();
            var products = query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();

            var data = _mapper.Map<List<Data>>(products);

            return new GetAllProductViewModel { TotalRecord = totalRecord, datas = data };
        }
        catch (Exception ex)
        {
            return new GetAllProductViewModel { TotalRecord = 0, datas = new List<Data>() };
        }
    }
}
