
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Task.Application.Commands
{
    public class LoginUserCommand : IRequest<bool>
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
