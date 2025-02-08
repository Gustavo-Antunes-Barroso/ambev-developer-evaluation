using Ambev.DeveloperEvaluation.Application.Shared.Results;
using AutoBogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Result
{
    public static class UpsertSaleResultData
    {
        public static UpsertSaleResult GenerateRandomUpsertSaleResult()
            => AutoFaker.Generate<UpsertSaleResult>();
    }
}
