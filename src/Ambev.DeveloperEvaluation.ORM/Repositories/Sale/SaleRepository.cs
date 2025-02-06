using domainEntity = Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.ORM.Repositories.Sale
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;
        private readonly IMongoClient _mongoClient;
        private IMongoCollection<domainEntity.Sale> _collection;

        public SaleRepository(DefaultContext context, IMongoClient mongoClient)
        {
            _context = context;
            _mongoClient = mongoClient;
            _collection = _mongoClient.GetDatabase("Ambev").GetCollection<domainEntity.Sale>(nameof(domainEntity.Sale));
        }
        #region <<<Postgres>>>
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

            _context.Remove(sale);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> UpdateAsync(domainEntity.Sale obj, CancellationToken cancellationToken = default)
        {
            domainEntity.Sale? sale = await GetByIdAsync(obj.Id, cancellationToken);

            if (sale is not null)
            {
                sale = obj;
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }

            return false;
        }

        public async Task<IList<domainEntity.Sale>?> GetAllAsync(int page, int quantity, CancellationToken cancellationToken = default)
        {
            return await _context.Sales
                .Skip((page - 1) * quantity)
                .Take(quantity)
                .ToListAsync(cancellationToken);
        }

        public async Task<domainEntity.Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Sales.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<bool> CancelSaleAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sale = await GetByIdAsync(id, cancellationToken);
            if (sale is null)
                return false;

            sale.Canceled = true;
            await UpdateAsync(sale, cancellationToken);
            return true;
        }
        #endregion

        #region<<<MongoDb>>>
        public async Task<domainEntity.Sale> MongoDbCreateAsync(domainEntity.Sale obj, CancellationToken cancellationToken = default)
        {
            await _collection.InsertOneAsync(obj);
            return obj;
        }

        public async Task<domainEntity.Sale> MongoDbUpdateAsync(domainEntity.Sale obj, CancellationToken cancellationToken = default)
        {
            var filter = Builders<domainEntity.Sale>.Filter.Eq("id", obj.Id);
            await _collection.ReplaceOneAsync(filter, obj);
            return obj;
        }

        public async Task<domainEntity.Sale> MongoDbDeleteAsync(domainEntity.Sale obj, CancellationToken cancellationToken = default)
        {
            var filter = Builders<domainEntity.Sale>.Filter.Eq("id", obj.Id);
            await _collection.DeleteOneAsync(filter, cancellationToken);
            return obj;
        }

        public domainEntity.Sale MongoDbGet(Guid id, CancellationToken cancellationToken = default)
        {
            var filter = Builders<domainEntity.Sale>.Filter.Eq("id", id);
            return (domainEntity.Sale)_collection.Find(filter);
        }

        public async Task<IList<domainEntity.Sale>> MongoDbGetAllAsync(CancellationToken cancellationToken = default)
        {
            var response = await _collection.FindAsync("{}");
            return response.ToList();
        }
        #endregion
    }
}
