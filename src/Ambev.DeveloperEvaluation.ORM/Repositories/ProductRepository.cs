using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DefaultContext _context;

        public ProductRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateAsync(Product obj, CancellationToken cancellationToken = default)
        {
            await _context.Product.AddAsync(obj, cancellationToken);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Product? product = await GetByIdAsync(id, cancellationToken);
            if (product is null)
                return false;

            _context.Product.Remove(product);
            return _context.SaveChangesAsync().Result > 0;
        }

        public async Task<IList<Product>?> GetAllAsync(int page, int quantity, CancellationToken cancellationToken = default)
        {
            return await _context.Product
                .Skip((page - 1) * quantity)
                .Take(quantity)
                .ToListAsync(cancellationToken);
        }

        public async Task<Product?> GetByDescriptionAsync(string description, CancellationToken cancellationToken = default)
        {
            return await _context.Product.FirstOrDefaultAsync(x => x.Description == description, cancellationToken);
        }

        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Product.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
