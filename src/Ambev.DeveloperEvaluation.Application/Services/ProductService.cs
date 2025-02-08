using Ambev.DeveloperEvaluation.Application.Shared.Commands;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Services
{
    public class ProductService(IProductRepository productRepository, IMapper mapper)
        : IProductService<UpsertSaleCommand>
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<SaleProduct[]> GetAndValidateUpsertSaleProducts(UpsertSaleCommand command, CancellationToken cancellationToken)
        {
            List<SaleProduct> productsResponse = new List<SaleProduct>();
            List<string> productsNotFound = new List<string>();

            for (int i = 0; i < command.Products.Length; i++)
            {
                UpsertSaleProductCommand processingProduct = command.Products[i];

                Product? productDb = await _productRepository.GetByIdAsync(processingProduct.ProductId, cancellationToken);

                if (productDb is null)
                {
                    productsNotFound.Add(processingProduct.ProductId.ToString());
                    continue;
                }

                SaleProduct productResult = _mapper.Map<SaleProduct>(productDb);
                SetSaleProductProperties(processingProduct, productResult);
                productsResponse.Add(productResult);
            }

            if (productsNotFound.Count() > 0)
                throw new KeyNotFoundException($"Products with IDs {string.Join(",", productsNotFound)} not found");

            return productsResponse.ToArray();
        }

        public void CalculateProductsValues(SaleProduct[] products, decimal discount, CancellationToken cancellationToken)
        {
            Parallel.ForEach(products, (product, cancellationToken) =>
            {
                product.CalculateSale(discount);
            });
        }

        public void ValidateClientValues(UpsertSaleCommand command, Sale sale)
        {
            if (sale.TotalAmount != command.TotalAmount)
                throw new InvalidOperationException($"Sale total amount {command.TotalAmount} is wrong! Correct value is {sale.TotalAmount}");

            if (sale.TotalAmountWithDiscount != command.TotalAmountWithDiscount)
                throw new InvalidOperationException($"Sale total amount with discount {command.TotalAmountWithDiscount} is wrong! Correct value is {sale.TotalAmountWithDiscount}");

            IEnumerable<object>? saleProperties =
                sale.Products.Select(x => new
                {
                    x.ProductId,
                    x.Price,
                    x.TotalAmount,
                    x.TotalAmountWithDiscount,
                    x.Discount
                });

            IEnumerable<object>? commandProperties =
                command.Products.Select(x => new
                {
                    x.ProductId,
                    x.Price,
                    x.TotalAmount,
                    x.TotalAmountWithDiscount,
                    x.Discount
                });

            IEnumerable<object>? wrongValuesProducts = saleProperties?.Except(commandProperties);

            if (wrongValuesProducts is not null && wrongValuesProducts.Count() > 0)
                throw new InvalidOperationException($"Products with wrong values: {string.Join(",", wrongValuesProducts.Select(x => x))}");
        }

        private void SetSaleProductProperties(UpsertSaleProductCommand processingProduct, SaleProduct productResult)
        {
            productResult.Quantity = processingProduct.Quantity;
            productResult.Discount = processingProduct.Discount;
            productResult.TotalAmount = processingProduct.TotalAmount;
            productResult.TotalAmountWithDiscount = processingProduct.TotalAmountWithDiscount;
        }
    }
}
