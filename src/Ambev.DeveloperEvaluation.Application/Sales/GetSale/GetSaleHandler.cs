using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleHandler(ISaleRepository saleRepository, ISaleProductRepository productRepository, IMapper mapper)
        : IRequestHandler<GetSaleCommand, GetSaleResult>
    {
        private readonly ISaleRepository _saleRepository = saleRepository;
        private readonly ISaleProductRepository _productRepository = productRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<GetSaleResult> Handle(GetSaleCommand request, CancellationToken cancellationToken)
        {
            GetSaleCommandValidator validator = new GetSaleCommandValidator();
            ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            Sale? result = await _saleRepository.MongoDbGetAsync(request.Id.ToString(), cancellationToken);

            if (!ValidateIfResultIsNull(result))
                return _mapper.Map<GetSaleResult>(result);

            result = await _saleRepository.GetCompleteSaleByIdAsync(request.Id, cancellationToken);

            if (!ValidateIfResultIsNull(result))
                return _mapper.Map<GetSaleResult>(result);

            throw new KeyNotFoundException($"Sale Id: {request.Id} not found");
        }

        private bool ValidateIfResultIsNull(Sale? result)
            => result is null;
    }
}
