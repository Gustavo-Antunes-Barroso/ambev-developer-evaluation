using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleCommandValidator : AbstractValidator<GetSaleCommand>
    {
        public GetSaleCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotEqual(Guid.Empty)
                .WithMessage("Id cannot be emprty");
        }
    }
}
