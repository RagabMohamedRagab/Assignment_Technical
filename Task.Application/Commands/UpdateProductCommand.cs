using MediatR;

namespace Task.Application.Commands
{
    public class UpdateProductCommand:IRequest<bool>
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int Quantity {  get; set; }
    }
}
