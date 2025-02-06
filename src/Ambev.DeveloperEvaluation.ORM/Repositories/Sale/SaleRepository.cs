using domainEntity = Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories.Sale
{
    public class SaleRepository(DefaultContext context)
        : ISaleRepository
    {
        private readonly DefaultContext _context = context;
            
        public async Task<domainEntity.Sale> CreateAsync(domainEntity.Sale obj, CancellationToken cancellationToken = default)
        {
            await _context.Sales.AddAsync(obj, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return obj;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            domainEntity.Sale? sale = await GetByIdAsync(id, cancellationToken);

            if (sale is null)
                return false;

            sale.Canceled = true;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> UpdateAsync(domainEntity.Sale obj, CancellationToken cancellationToken = default)
        {
            domainEntity.Sale? sale = await _context.Sales.FirstOrDefaultAsync(x => x.Id == obj.Id);

            if (sale is not null)
            {
                sale = obj;
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }

            return false;
        }

        public Task<IList<domainEntity.Sale>?> GetAllAsync(int page, int quantity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<domainEntity.Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
