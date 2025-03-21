using MediatR;
using Task.Application.ViewModel;

namespace Task.Application.Queries
{
    public class GetAllProductQuery:IRequest<GetAllProductViewModel>
    {
        public int PageNumber {  get; set; }
        public int PageSize {  get; set; }
        public string? SearchValue {  get; set; }
        public string? SortColumn {  get; set; }
        public string? SortDirection {  get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int Draw { get; set; } = 0;


    }
}
