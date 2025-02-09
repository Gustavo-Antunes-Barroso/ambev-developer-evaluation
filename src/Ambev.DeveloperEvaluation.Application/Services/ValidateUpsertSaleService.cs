using Ambev.DeveloperEvaluation.Application.Shared.Commands;
using Ambev.DeveloperEvaluation.Application.Shared.Validator;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Services
{
    public class ValidateUpsertSaleService(IUserService userService, ISubsidiaryService subsidiaryService, 
        IProductService<UpsertSaleCommand> productService, IMapper mapper)
        : IValidateUpsertSaleService<UpsertSaleCommand>
    {
        private readonly IUserService _userService = userService;
        private readonly ISubsidiaryService _subsidiaryService = subsidiaryService;
        private readonly IProductService<UpsertSaleCommand> _productService = productService;
        private readonly IMapper _mapper = mapper;

        public async Task<Sale> ValidateUpsertSaleAsync(UpsertSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpsertSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            User customer = await _userService.GetAndValidateUser(command.CustomerId, cancellationToken);
            Subsidiary subsidiary = await _subsidiaryService.GetAndValidateSubsidiary(command.SubsidiaryId, cancellationToken);
            SaleProduct[] products = await _productService.GetAndValidateUpsertSaleProducts(command, cancellationToken);

            Sale sale = _mapper.Map<Sale>(command);

            sale.SetCustomer(_mapper.Map<SaleCustomer>(customer))
                .SetSubsidiary(_mapper.Map<SaleSubsidiary>(subsidiary))
                .SetProducts(products); //Setting products to calculate discount value

            _productService.CalculateProductsValues(products, sale.GetDiscount(), cancellationToken);

            //Update products with values calculated
            sale.SetProducts(products)
                .CalculateSale();

            _productService.ValidateClientValues(command, sale);

            return sale;
        }
    }
}
