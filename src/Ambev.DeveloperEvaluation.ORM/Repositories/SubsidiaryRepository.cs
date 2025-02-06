using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SubsidiaryRepository(DefaultContext context)
        : ISubsidiaryRepository
    {
        private readonly DefaultContext _context = context;

        public async Task<Subsidiary> CreateAsync(Subsidiary obj, CancellationToken cancellationToken = default)
        {
            await _context.Subsidiaries.AddAsync(obj, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return obj;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Subsidiary? subsidiary = await GetByIdAsync(id, cancellationToken);
            if (subsidiary is null)
                return false;

            _context.Subsidiaries.Remove(subsidiary);
            return _context.SaveChangesAsync().Result > 0;
        }

        public async Task<IList<Subsidiary>?> GetAllAsync(int page, int quantity, CancellationToken cancellationToken = default)
        {
            return await _context.Subsidiaries
                .Skip((page - 1) * quantity)
                .Take(quantity)
                .ToListAsync(cancellationToken);
        }

        public async Task<Subsidiary?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Subsidiaries.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<bool> UpdateAsync(Subsidiary obj, CancellationToken cancellationToken = default)
        {
            Subsidiary? subsidiary = await GetByIdAsync(obj.Id, cancellationToken);

            if (subsidiary is null)
                return false;

            subsidiary = obj;
            _context.SaveChanges();
            return true;
        }
    }
}
