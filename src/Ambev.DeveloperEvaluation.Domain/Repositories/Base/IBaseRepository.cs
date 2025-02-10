namespace Ambev.DeveloperEvaluation.Domain.Repositories.Base
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> CreateAsync(T obj, CancellationToken cancellationToken = default);
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IList<T>?> GetAllAsync(int page, int quantity, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(T obj, CancellationToken cancellationToken = default);
    }
}
