using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Repositories.Base;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository : IBaseRepository<Sale>
    {
        Task<bool> CancelSaleAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Sale> MongoDbCreateAsync(Sale obj, CancellationToken cancellationToken = default);
        Task<Sale> MongoDbUpdateAsync(Sale obj, CancellationToken cancellationToken = default);
        Task<bool> MongoDbDeleteAsync(string id, CancellationToken cancellationToken = default);
        Sale MongoDbGet(string id, CancellationToken cancellationToken = default);
        Task<IList<Sale>> MongoDbGetAllAsync(CancellationToken cancellationToken = default);
    }
}
