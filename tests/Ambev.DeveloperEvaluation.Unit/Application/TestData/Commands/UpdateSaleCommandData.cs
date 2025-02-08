using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using AutoBogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Commands
{
    public static class UpdateSaleCommandData
    {
        public static UpdateSaleCommand GenerateValidRandomUpdateSaleCommand()
            => AutoFaker.Generate<UpdateSaleCommand>();
    }
}
