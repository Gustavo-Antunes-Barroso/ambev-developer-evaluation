using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;

namespace Ambev.DeveloperEvaluation.Application.Services
{
    public class UserService(IUserRepository userRepository)
        : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<User> GetAndValidateUser(Guid id, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetByIdAsync(id, cancellationToken);

            if (user is null)
                throw new KeyNotFoundException($"User with ID {id} not found");

            return user;
        }
    }
}
