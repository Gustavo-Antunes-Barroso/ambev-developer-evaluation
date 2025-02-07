using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using AutoBogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleHandlersTest.CommomData
{
    public static class SaleEntityData
    {
        public static Sale GenerateRandomSaleData()
            => AutoFaker.Generate<Sale>();
    }
}
