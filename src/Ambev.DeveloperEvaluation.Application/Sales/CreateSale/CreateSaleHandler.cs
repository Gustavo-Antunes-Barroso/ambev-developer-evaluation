using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler(ISaleRepository saleRepository, IUserRepository userRepository,
        IProductRepository productRepository, ISubsidiaryRepository subsidiaryRepository,
        ISaleProductRepository saleProductRepository,IMapper mapper)
        : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository = saleRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IProductRepository _productRepository = productRepository;
        private readonly ISubsidiaryRepository _subsidiaryRepository = subsidiaryRepository;
        private readonly ISaleProductRepository _saleProductRepository = saleProductRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            User customer = await GetAndValidateUser(request.CustomerId, cancellationToken);
            Subsidiary subsidiary = await GetAndValidateSubsidiary(request.SubsidiaryId, cancellationToken);
            SaleProduct[] products = await GetAndValidateProducts(request.Products, cancellationToken);

            Sale sale = _mapper.Map<Sale>(request);

            sale.SetCustomer(_mapper.Map<SaleCustomer>(customer))
                .SetSubsidiary(_mapper.Map<SaleSubsidiary>(subsidiary))
                .SetProducts(products);

            CalculateProductsValues(products, sale.GetDiscount(), cancellationToken);

            //Update products values
            sale.SetProducts(products)
                .CalculateSale();

            ValidateClientValues(request, sale);

            CreateSaleResult response = await CreateSaleAsync(sale, cancellationToken);
            return response;
        }

        private async Task<User> GetAndValidateUser(Guid id, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetByIdAsync(id, cancellationToken);

            if (user is null)
                throw new KeyNotFoundException($"User with ID {id} not found");

            return user;
        }

        private async Task<Subsidiary> GetAndValidateSubsidiary(Guid id, CancellationToken cancellationToken)
        {
            Subsidiary subsidiary = await _subsidiaryRepository.GetByIdAsync(id, cancellationToken);

            if (subsidiary is null)
                throw new KeyNotFoundException($"Subsidiary with ID {id} not found");

            return subsidiary;
        }

        private async Task<SaleProduct[]> GetAndValidateProducts(CreateSaleProductsCommand[] products, CancellationToken cancellationToken)
        {
            List<SaleProduct> productsResponse = new List<SaleProduct>();
            List<string> productsNotFound = new List<string>();

            for (int i = 0; i < products.Length; i++)
            {
                CreateSaleProductsCommand processingProduct = products[i];

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

        private void SetSaleProductProperties(CreateSaleProductsCommand processingProduct, SaleProduct productResult)
        {
            productResult.Quantity = processingProduct.Quantity;
            productResult.Discount = processingProduct.Discount;
            productResult.TotalAmount = processingProduct.TotalAmount;
            productResult.TotalAmountWithDiscount = processingProduct.TotalAmountWithDiscount;
        }

        private void CalculateProductsValues(SaleProduct[] products, decimal discount, CancellationToken cancellationToken)
        {
            Parallel.ForEach(products, (product, cancellationToken) =>
            {
                product.CalculateSale(discount);
            });
        }

        private void ValidateClientValues(CreateSaleCommand command, Sale sale)
        {
            if(sale.TotalAmount != command.TotalAmount)
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

            if(wrongValuesProducts is not null && wrongValuesProducts.Count() > 0)
                throw new InvalidOperationException($"Products with wrong values: {string.Join(",", wrongValuesProducts.Select(x => x))}");
        }

        private async Task<CreateSaleResult> CreateSaleAsync(Sale sale, CancellationToken cancellationToken)
        {
            Sale saleResult = await _saleRepository.CreateAsync(sale, cancellationToken);
            saleResult.SetProductsSaleId(sale.Id);
            SaleProduct[] saleProductsResult = await _saleProductRepository.CreateManyAsync(sale.Products, cancellationToken);

            saleResult.SetProducts(saleProductsResult);
            return _mapper.Map<CreateSaleResult>(saleResult);
        }
    }
}
