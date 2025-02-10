using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class ProductRepository(DefaultContext context)
        : IProductRepository
    {
        private readonly DefaultContext  _context = context;
        
        public async Task<Product> CreateAsync(Product obj, CancellationToken cancellationToken = default)
        {
            await _context.Products.AddAsync(obj, cancellationToken);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Product? product = await GetByIdAsync(id, cancellationToken);
            if (product is null)
                return false;

            _context.Products.Remove(product);
            return _context.SaveChangesAsync().Result > 0;
        }

        public async Task<IList<Product>?> GetAllAsync(int page, int quantity, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .Skip((page - 1) * quantity)
                .Take(quantity)
                .ToListAsync(cancellationToken);
        }

        public async Task<Product?> GetByDescriptionAsync(string description, CancellationToken cancellationToken = default)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Description == description, cancellationToken);
        }

        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<bool> UpdateAsync(Product obj, CancellationToken cancellationToken = default)
        {
            Product? product = await GetByIdAsync(obj.Id, cancellationToken);

            if (product is null)
                return false;

            _context.Products.Entry(product).State = EntityState.Detached;
            product = obj;
            _context.Products.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
