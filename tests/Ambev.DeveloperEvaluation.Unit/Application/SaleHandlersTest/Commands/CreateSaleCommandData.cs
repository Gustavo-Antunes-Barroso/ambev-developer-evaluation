using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using AutoBogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleHandlersTest.Commands
{
    public static class CreateSaleCommandData
    {
        public static CreateSaleCommand GenerateValidRandomSaleCommand()
            => AutoFaker.Generate<CreateSaleCommand>();
    }
}
