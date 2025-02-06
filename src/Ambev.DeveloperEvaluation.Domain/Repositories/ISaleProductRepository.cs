using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Repositories.Base;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleProductRepository : IBaseRepository<SaleProduct>
    {
        Task<bool> DeleteBySaleIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<SaleProduct[]> GetBySaleIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<SaleProduct[]> CreateManyAsync(SaleProduct[] products, CancellationToken cancellationToken = default);
    }
}
