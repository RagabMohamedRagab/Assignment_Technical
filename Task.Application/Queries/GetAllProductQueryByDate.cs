
using MediatR;
using Task.Application.ViewModel;

namespace Task.Application.Queries
{
    public class GetAllProductQueryByDate:IRequest<GetAllProductViewModel>
    {
        public DateTime? From {  get; set; }
        public DateTime? To {  get; set; }
    }
}
