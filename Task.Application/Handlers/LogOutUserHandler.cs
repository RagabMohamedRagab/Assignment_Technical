

using MediatR;
using Task.Application.Queries;
using Task.DataAccess.IRepositories;

namespace Task.Application.Handlers
{
    public class LogOutUserHandler (IAuthRepository authRepository) : IRequestHandler<LogOutUserQuery, bool>
    {
        private readonly IAuthRepository _authRepository = authRepository;
        public Task<bool> Handle(LogOutUserQuery request, CancellationToken cancellationToken)
        {
            return _authRepository.LogOut();
        }
    }
}
