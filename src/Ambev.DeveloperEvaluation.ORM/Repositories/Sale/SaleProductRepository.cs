﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories.Sale
{
    public class SaleProductRepository(DefaultContext context)
        : ISaleProductRepository
    {
        private readonly DefaultContext _context = context;

        public async Task<SaleProduct> CreateAsync(SaleProduct obj, CancellationToken cancellationToken = default)
        {
            await _context.SaleProducts.AddAsync(obj, cancellationToken);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<SaleProduct[]> CreateManyAsync(SaleProduct[] products, CancellationToken cancellationToken = default)
        {
            await _context.SaleProducts.AddRangeAsync(products, cancellationToken);
            await _context.SaveChangesAsync();
            return products;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            SaleProduct? saleProduct = await GetByIdAsync(id, cancellationToken);

            if (saleProduct is null)
                return false;

            _context.SaleProducts.Remove(saleProduct);
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteBySaleIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            IEnumerable<SaleProduct> saleProduct = await _context.SaleProducts.Where(x => x.SaleId == id).ToListAsync(cancellationToken);

            if (saleProduct is null || saleProduct.Count() == 0)
                return false;

            _context.SaleProducts.RemoveRange(saleProduct);
            _context.SaveChanges();
            return true;
        }

        public async Task<IList<SaleProduct>?> GetAllAsync(int page, int quantity, CancellationToken cancellationToken = default)
        {
            return await _context.SaleProducts
                .Skip((page - 1) * quantity)
                .Take(quantity)
                .ToListAsync(cancellationToken);
        }

        public async Task<SaleProduct[]> GetBySaleIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _context.SaleProducts.Where(x => x.SaleId == id).ToArrayAsync(cancellationToken);

        public async Task<SaleProduct?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _context.SaleProducts.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<bool> UpdateAsync(SaleProduct obj, CancellationToken cancellationToken = default)
        {
            SaleProduct? saleProduct = await GetByIdAsync(obj.Id, cancellationToken);

            if (saleProduct is null)
                return false;
            _context.SaleProducts.Entry(saleProduct).State = EntityState.Detached;
            saleProduct = obj;
            _context.SaleProducts.Entry(saleProduct).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
