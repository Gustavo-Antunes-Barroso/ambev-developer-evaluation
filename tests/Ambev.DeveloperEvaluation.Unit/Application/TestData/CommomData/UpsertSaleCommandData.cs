using Ambev.DeveloperEvaluation.Application.Shared.Commands;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoBogus;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.CommomData
{
    public static class UpsertSaleCommandData
    {
        public static UpsertSaleCommand GenerateValidRandomUpsertSaleCommand()
        {
            Faker<UpsertSaleCommand> faker = new Faker<UpsertSaleCommand>();

            var command =  AutoFaker.Generate<UpsertSaleCommand>();
            command.Products = GenerateValidRandomListUpsertSaleProductCommand();
            return command;
        }

        private static UpsertSaleProductCommand[] GenerateValidRandomListUpsertSaleProductCommand()
        {
            Faker<UpsertSaleProductCommand> faker = new Faker<UpsertSaleProductCommand>()
                .RuleFor(x => x.Quantity, x => x.Random.Number(1, 20))
                .RuleFor(x => x.Price, x => x.Random.Decimal(000.01M, 200.00M))
                .RuleFor(x => x.Discount, x => 10)
                .RuleFor(x => x.TotalAmount, x => x.Random.Decimal(10, 200))
                .RuleFor(x => x.TotalAmountWithDiscount, x => x.Random.Decimal(10, 200));

            return new[]
            { 
                faker.Generate(), 
                faker.Generate(), 
                faker.Generate() 
            };
        }
    }
}
