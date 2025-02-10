using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    public class DeleteSaleHandler(ISaleRepository saleRepository, ISaleProductRepository saleProductRepository, IRabbitMQProducer<Sale> rabbitMQProducer)
        : IRequestHandler<DeleteSaleCommand, DeleteSaleResult>
    {
        private readonly ISaleRepository _saleRepository = saleRepository;
        private readonly ISaleProductRepository _saleProductRepository = saleProductRepository;
        private readonly IRabbitMQProducer<Sale> _rabbitMQProducer = rabbitMQProducer;

        public async Task<DeleteSaleResult> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            DeleteSaleCommandValidator commandValidator = new();
            ValidationResult validationResult = await commandValidator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            await _saleRepository.MongoDbDeleteAsync(request.Id.ToString(), cancellationToken);
            await DeleteSaleFromPostegres(request.Id, cancellationToken);

            return new DeleteSaleResult { Sucess = true };
        }
        private async Task DeleteSaleFromPostegres(Guid id, CancellationToken cancellationToken)
        {
            await _saleProductRepository.DeleteBySaleIdAsync(id, cancellationToken);
            await _saleRepository.DeleteAsync(id, cancellationToken);
            await _rabbitMQProducer.SendMessage(new Sale { Id = id }, HttpMethod.Delete.ToString());
        }
    }
}
