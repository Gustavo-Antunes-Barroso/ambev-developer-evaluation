using Ambev.DeveloperEvaluation.Application.Shared.Commands;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Shared.Validator
{
    public class UpsertSaleCommandValidator : AbstractValidator<UpsertSaleCommand>
    {
        public UpsertSaleCommandValidator()
        {
            RuleFor(x => x.Products)
                .NotEmpty()
                .NotNull()
                .WithMessage("List of products cannot be empty");

            RuleForEach(x => x.Products)
                .ChildRules(product =>
                {
                    product.RuleFor(x => x.Quantity)
                    .LessThanOrEqualTo(20)
                    .WithMessage("Quantity of a product cannot be greater than 20");
                });

            RuleFor(x => x.SubsidiaryId)
                .NotNull()
                .NotEqual(Guid.Empty)
                .WithMessage("Subsidiary Id is not valid");

            RuleFor(x => x.CustomerId)
                .NotNull()
                .NotEqual(Guid.Empty)
                .WithMessage("Customer Id is not valid");

            RuleFor(x => x.TotalAmount)
                .GreaterThan(0)
                .NotNull()
                .WithMessage("Total of sale amount cannot be less than 0");
        }
    }
}
