using Ambev.DeveloperEvaluation.Application.Shared.Commands;
using Ambev.DeveloperEvaluation.Application.Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler(ISaleRepository saleRepository,ISaleProductRepository saleProductRepository,
        IValidateUpsertSaleService<UpsertSaleCommand> validateUpsertSaleService, IMapper mapper)
        : IRequestHandler<CreateSaleCommand, UpsertSaleResult>
    {
        private readonly ISaleRepository _saleRepository = saleRepository;
        private readonly ISaleProductRepository _saleProductRepository = saleProductRepository;
        private readonly IValidateUpsertSaleService<UpsertSaleCommand> _validateUpsertSaleService = validateUpsertSaleService;
        private readonly IMapper _mapper = mapper;

        public async Task<UpsertSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            UpsertSaleCommand upsertSaleCommand = _mapper.Map<UpsertSaleCommand>(request);
            Sale sale = await _validateUpsertSaleService.ValidateUpsertSaleAsync(upsertSaleCommand, cancellationToken);
            UpsertSaleResult response = await CreateSaleAsync(sale, cancellationToken);

            return response;
        }

        private async Task<UpsertSaleResult> CreateSaleAsync(Sale sale, CancellationToken cancellationToken)
        {
            Sale saleResult = await _saleRepository.CreateAsync(sale, cancellationToken);
            saleResult.SetProductsSaleId(sale.Id);
            SaleProduct[] saleProductsResult = await _saleProductRepository.CreateManyAsync(sale.Products, cancellationToken);

            saleResult.SetProducts(saleProductsResult);
            await _saleRepository.MongoDbCreateAsync(saleResult, cancellationToken);
            return _mapper.Map<UpsertSaleResult>(saleResult);
        }
    }
}
