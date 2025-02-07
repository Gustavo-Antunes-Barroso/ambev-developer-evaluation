using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public interface IUserService
    {
        Task<User> GetAndValidateUser(Guid id, CancellationToken cancellationToken);
    }
}