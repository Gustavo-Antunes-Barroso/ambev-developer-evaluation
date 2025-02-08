using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using AutoBogus;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Entities
{
    public static class SaleProductEntityData
    {
        public static SaleProduct GenerateRandomSaleProductData()
            => AutoFaker.Generate<SaleProduct>();

        public static SaleProduct[] GenerateRandomListSaleProductData()
        {
            Faker<SaleProduct> faker = new Faker<SaleProduct>()
                .RuleFor(u => u.Price, f => f.Random.Decimal(000.1M, 999.99M))
                .RuleFor(u => u.Quantity, f => f.Random.Number(1, 20));

            SaleProduct[] products = new[] { faker.Generate(), faker.Generate(), faker.Generate() };

            return products;
        }
    }
}
