using Ambev.DeveloperEvaluation.Domain.Entities.Sale;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public interface IProductService<T> where T : class
    {
        Task<SaleProduct[]> GetAndValidateProducts(T command, CancellationToken cancellationToken);
        void CalculateProductsValues(SaleProduct[] products, decimal discount, CancellationToken cancellationToken);
        void ValidateClientValues(T command, Sale sale);
    }
}
