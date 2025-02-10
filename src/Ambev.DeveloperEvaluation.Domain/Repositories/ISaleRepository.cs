using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Repositories.Base;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository : IBaseRepository<Sale>
    {
        Task<Sale?> GetCompleteSaleByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> CancelSaleAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Sale> MongoDbCreateAsync(Sale obj, CancellationToken cancellationToken = default);
        Task<bool> MongoDbUpdateAsync(Sale obj, CancellationToken cancellationToken = default);
        Task<bool> MongoDbDeleteAsync(string id, CancellationToken cancellationToken = default);
        Task<Sale> MongoDbGetAsync(string id, CancellationToken cancellationToken = default);
        Task<IList<Sale>> MongoDbGetAllAsync(CancellationToken cancellationToken = default);
    }
}
