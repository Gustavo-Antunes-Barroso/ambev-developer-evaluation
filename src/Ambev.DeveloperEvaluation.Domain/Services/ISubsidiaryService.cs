using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public interface ISubsidiaryService
    {
        Task<Subsidiary> GetAndValidateSubsidiary(Guid id, CancellationToken cancellationToken);
    }
}
