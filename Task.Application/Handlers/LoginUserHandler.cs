
using MediatR;
using Task.Application.Commands;
using Task.DataAccess.IRepositories;

namespace Task.Application.Handlers
{
    public class LoginUserHandler(IAuthRepository authRepository) : IRequestHandler<LoginUserCommand, bool>
    {
        private readonly IAuthRepository _authRepository = authRepository;

        public async Task<bool> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _authRepository.AutenticatedUser(request.Email, request.Password);
                return response;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
