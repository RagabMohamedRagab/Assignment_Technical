
using MediatR;
using Task.Application.Commands;
using Task.Core.Entities;
using Task.DataAccess.IRepositories;

namespace Task.Application.Handlers
{
    public class CreateUserRegisterHandler(IAuthRepository authRepository) : IRequestHandler<CreateUserRegisterCommand, bool>
    {
        private readonly IAuthRepository _authRepository = authRepository;

        public async Task<bool> Handle(CreateUserRegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                user user = new() { Email = request.Email, UserName = request.Email, Name = request.Name };
                var response = await _authRepository.CreateAsync(user, request.Password);
                return response;
            }
            catch (Exception ex) { return false; }
        }
    }
}
