using Ambev.DeveloperEvaluation.Domain.Entities.Sale;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public interface IValidateUpsertSaleService<T> where T : class
    {
        Task<Sale> ValidateUpsertSaleAsync(T command, CancellationToken cancellationToken);
    }
}
