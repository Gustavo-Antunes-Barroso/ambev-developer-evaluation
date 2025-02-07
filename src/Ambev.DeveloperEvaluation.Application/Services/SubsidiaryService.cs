using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;

namespace Ambev.DeveloperEvaluation.Application.Services
{
    public class SubsidiaryService(ISubsidiaryRepository subsidiaryRepository)
        : ISubsidiaryService
    {
        private readonly ISubsidiaryRepository _subsidiaryRepository = subsidiaryRepository;
        public async Task<Subsidiary> GetAndValidateSubsidiary(Guid id, CancellationToken cancellationToken)
        {
            Subsidiary? subsidiary = await _subsidiaryRepository.GetByIdAsync(id, cancellationToken);

            if (subsidiary is null)
                throw new KeyNotFoundException($"Subsidiary with ID {id} not found");

            return subsidiary;
        }
    }
}
