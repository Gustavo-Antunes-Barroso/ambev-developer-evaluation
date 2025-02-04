using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    public class CreateProductHandler(IProductRepository productRepository, IMapper mapper)
        : IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Handles the CreateProductCommand request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            CreateProductCommandValidator validator = new();
            ValidationResult validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            Product? existingProduct = await _productRepository.GetByDescriptionAsync(request.Description, cancellationToken);
            if(existingProduct is not null)
                throw new InvalidOperationException($"Product with description {request.Description} already exists.");

            Product product = _mapper.Map<Product>(request);

            Product createdProduct = await _productRepository.CreateAsync(product, cancellationToken);
            CreateProductResult result = _mapper.Map<CreateProductResult>(createdProduct);
            return result;
        }
    }
}
