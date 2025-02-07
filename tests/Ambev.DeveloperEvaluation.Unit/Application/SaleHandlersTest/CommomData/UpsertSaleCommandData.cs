using Ambev.DeveloperEvaluation.Application.Shared.Commands;
using AutoBogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleHandlersTest.CommomData
{
    public static class UpsertSaleCommandData
    {
        public static UpsertSaleCommand GenerateValidRandomUpsertSaleCommand()
            => AutoFaker.Generate<UpsertSaleCommand>();
    }
}
