using Ambev.DeveloperEvaluation.Application.Shared.Commands;
using AutoBogus;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.CommomData
{
    public static class UpsertSaleCommandData
    {
        public static UpsertSaleCommand GenerateValidRandomUpsertSaleCommand()
        {
            Faker<UpsertSaleCommand> faker = new Faker<UpsertSaleCommand>()
                .RuleFor(x => x.Products, x 
                => x.Random.ListItems(GenerateValidRandomListUpsertSaleProductCommand(), 3));

            return AutoFaker.Generate<UpsertSaleCommand>();
        }

        private static List<UpsertSaleProductCommand> GenerateValidRandomListUpsertSaleProductCommand()
        {
            Faker<UpsertSaleProductCommand> faker = new Faker<UpsertSaleProductCommand>()
                .RuleFor(x => x.Quantity, x => x.Random.Number(1, 20))
                .RuleFor(x => x.Price, x => x.Random.Decimal(000.01M, 200.00M))
                .RuleFor(x => x.Discount, x => 10)
                .RuleFor(x => x.TotalAmount, x => x.Random.Decimal(10, 200))
                .RuleFor(x => x.TotalAmountWithDiscount, x => x.Random.Decimal(10, 200));

            var productCommands = new List<UpsertSaleProductCommand>{ faker.Generate(), faker.Generate(), faker.Generate() };
            return productCommands;
        }
    }
}
