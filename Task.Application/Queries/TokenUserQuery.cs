
using MediatR;
using Task.Application.Dtos;

namespace Task.Application.Queries
{
    public class TokenUserQuery:IRequest<AuthUserDto>
    {
    }
}
