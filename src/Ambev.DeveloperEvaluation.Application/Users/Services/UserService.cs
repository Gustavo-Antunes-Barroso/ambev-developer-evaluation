using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;

namespace Ambev.DeveloperEvaluation.Application.Users.Services
{
    public class UserService(IUserRepository userRepository)
        : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<bool> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            User? userEntity = await _userRepository.GetByIdAsync(id, cancellationToken);
            return userEntity is not null;
        }
    }
}
