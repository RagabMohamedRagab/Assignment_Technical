using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Task.Application.Commands
{
    public class CreateProductCommand:IRequest<bool>
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
