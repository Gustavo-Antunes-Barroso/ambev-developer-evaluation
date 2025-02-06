using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Repositories.Base;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleProductRepository : IBaseRepository<SaleProduct>
    {
        Task<SaleProduct[]> CreateManyAsync(SaleProduct[] products, CancellationToken cancellationToken = default);
    }
}
