using domainEntity = Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.ORM.Repositories.Sale
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;
        private readonly IMongoClient _mongoClient;
        private readonly IMapper _mapper;
        private IMongoCollection<domainEntity.Sale> _collection;

        public SaleRepository(DefaultContext context, IMongoClient mongoClient, IMapper mapper)
        {
            _context = context;
            _mongoClient = mongoClient;
            _mapper = mapper;
            _collection = _mongoClient.GetDatabase("Ambev").GetCollection<domainEntity.Sale>(nameof(domainEntity.Sale));
        }
        #region <<<Postgres>>>
        public async Task<domainEntity.Sale> CreateAsync(domainEntity.Sale obj, CancellationToken cancellationToken = default)
        {
            if (obj.CreatedAt.Equals(DateTime.MinValue))
                obj.CreatedAt = DateTime.UtcNow;
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
                _context.Sales.Entry(sale).State = EntityState.Detached;
                sale = obj;
                _context.Sales.Entry(sale).State = EntityState.Modified;
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

        public async Task<domainEntity.Sale?> GetCompleteSaleByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await (from s in _context.Sales
                                join sp in _context.SaleProducts on s.Id equals sp.SaleId into p
                                join sb in _context.Subsidiaries on s.SubsidiaryId equals sb.Id
                                join u in _context.Users on s.CustomerId equals u.Id
                                where s.Id == id
                                select new domainEntity.Sale
                                {
                                    Id = s.Id,
                                    Discount = s.Discount,
                                    SubsidiaryId = sb.Id,
                                    CustomerId = u.Id,
                                    Canceled = s.Canceled,
                                    CreatedAt = s.CreatedAt,
                                    TotalAmount = s.TotalAmount,
                                    TotalAmountWithDiscount = s.TotalAmountWithDiscount
                                }
                                .SetSubsidiary(_mapper.Map<SaleSubsidiary>(sb))
                                .SetProducts(_mapper.Map<SaleProduct[]>(p))
                                .SetCustomer(_mapper.Map<SaleCustomer>(u))
                                ).FirstOrDefaultAsync();
            return result;
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

        //TODO: Separar repositorios
        #region<<<MongoDb>>>
        public async Task<domainEntity.Sale> MongoDbCreateAsync(domainEntity.Sale obj, CancellationToken cancellationToken = default)
        {
            await _collection.InsertOneAsync(obj);
            return obj;
        }

        public async Task<bool> MongoDbUpdateAsync(domainEntity.Sale obj, CancellationToken cancellationToken = default)
        {
            var filter = Builders<domainEntity.Sale>.Filter.Eq("_id", obj.Id.ToString());
            await _collection.ReplaceOneAsync(filter, obj);
            return true;
        }

        public async Task<bool> MongoDbDeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var filter = Builders<domainEntity.Sale>.Filter.Eq("_id", id);
            await _collection.DeleteOneAsync(filter, cancellationToken);
            return true;
        }

        public async Task<domainEntity.Sale> MongoDbGetAsync(string id, CancellationToken cancellationToken = default)
        {
            var filter = Builders<domainEntity.Sale>.Filter.Eq("_id", id);
            var result = await _collection.Find(filter).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IList<domainEntity.Sale>> MongoDbGetAllAsync(CancellationToken cancellationToken = default)
        {
            var response = await _collection.FindAsync("{}");
            return response.ToList();
        }
        #endregion
    }
}
