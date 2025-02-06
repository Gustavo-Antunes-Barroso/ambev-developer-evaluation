using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    public class DeleteSaleHandler(ISaleRepository saleRepository, ISaleProductRepository saleProductRepository)
        : IRequestHandler<DeleteSaleCommand, DeleteSaleResult>
    {
        private readonly ISaleRepository _saleRepository = saleRepository;
        private readonly ISaleProductRepository _saleProductRepository = saleProductRepository;

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
        }
    }
}
