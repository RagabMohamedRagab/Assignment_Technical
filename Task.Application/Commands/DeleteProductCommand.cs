
using MediatR;

namespace Task.Application.Commands
{
    public class DeleteProductCommand:IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
