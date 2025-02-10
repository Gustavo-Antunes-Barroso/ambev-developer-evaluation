using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using AutoBogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Entities
{
    public static class SaleEntityData
    {
        public static Sale GenerateRandomSaleData()
            => AutoFaker.Generate<Sale>();
    }
}
