using Ambev.DeveloperEvaluation.Application.Shared.Commands;
using Ambev.DeveloperEvaluation.Application.Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler(ISaleRepository saleRepository, ISaleProductRepository saleProductRepository,
       IValidateUpsertSaleService<UpsertSaleCommand> validateUpsertSaleService, IRabbitMQProducer<Sale> rabbitMQProducer, IMapper mapper)
       : IRequestHandler<UpdateSaleCommand, UpsertSaleResult>
    {
        private readonly ISaleRepository _saleRepository = saleRepository;
        private readonly ISaleProductRepository _saleProductRepository = saleProductRepository;
        private readonly IValidateUpsertSaleService<UpsertSaleCommand> _validateUpsertSaleService = validateUpsertSaleService;
        private readonly IRabbitMQProducer<Sale> _rabbitMQProducer = rabbitMQProducer;
        private readonly IMapper _mapper = mapper;

        public async Task<UpsertSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            UpsertSaleCommand upsertSaleCommand = _mapper.Map<UpsertSaleCommand>(request);
            Sale sale = await _validateUpsertSaleService.ValidateUpsertSaleAsync(upsertSaleCommand, cancellationToken);
            UpsertSaleResult response = await UpdateSaleAsync(sale, cancellationToken);

            return response;
        }

        private async Task<UpsertSaleResult> UpdateSaleAsync(Sale sale, CancellationToken cancellationToken)
        {
            //Update Postgres
            await _saleRepository.UpdateAsync(sale, cancellationToken);
            await _saleProductRepository.DeleteBySaleIdAsync(sale.Id, cancellationToken);
            sale.SetProductsSaleId(sale.Id);
            SaleProduct[] saleProductsResult = await _saleProductRepository.CreateManyAsync(sale.Products, cancellationToken);

            //Update MongoDB
            await _saleRepository.MongoDbUpdateAsync(sale, cancellationToken);
            await _rabbitMQProducer.SendMessage(sale, HttpMethod.Put.ToString());
            return _mapper.Map<UpsertSaleResult>(sale);
        }
    }
}
