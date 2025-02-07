using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Shared.Commands;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpsertSale;
using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController(IMediator mediator, IMapper mapper)
        : BaseController
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;


        [HttpPost]
        public async Task<IActionResult> CreateSaleAsync([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
        {
            CreateSaleRequestValidator validator = new();
            ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateSaleCommand>(request);
            var result = await _mediator.Send(command);
            var response = _mapper.Map<UpsertSaleResponse>(result);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSaleAsync([FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
        {
            UpdateSaleRequestValidator validator = new();
            ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<UpdateSaleCommand>(request);
            var result = await _mediator.Send(command);
            var response = _mapper.Map<UpdateSaleResponse>(result);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSaleByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            GetSaleRequest request = new GetSaleRequest() { Id = id };
            GetSaleRequestValidator validator = new();
            ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<GetSaleCommand>(request);
            var result = await _mediator.Send(command);

            var response = _mapper.Map<GetSaleResponse>(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSaleAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            DeleteSaleRequest request = new DeleteSaleRequest() { Id = id };
            DeleteSaleRequestValidator validator = new();
            ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<DeleteSaleCommand>(request);
            var result = await _mediator.Send(command);

            return Ok("Sale deleted successfully");
        }
    }
}
