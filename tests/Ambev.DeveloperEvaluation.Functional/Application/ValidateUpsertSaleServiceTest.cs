using Ambev.DeveloperEvaluation.Application.Sales.Mapping;
using Ambev.DeveloperEvaluation.Application.Services;
using Ambev.DeveloperEvaluation.Application.Shared.Commands;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using NSubstitute;
using Xunit;
using Ambev.DeveloperEvaluation.Common.Util;

namespace Ambev.DeveloperEvaluation.Functional.Application
{
    public class ValidateUpsertSaleServiceTest
    {
        private readonly IUserService _userService;
        private readonly ISubsidiaryService _subsidiaryService;
        private readonly IProductService<UpsertSaleCommand> _productService;
        private readonly IMapper _mapper;
        private readonly ValidateUpsertSaleService _service;

        public ValidateUpsertSaleServiceTest()
        {
            _userService = Substitute.For<IUserService>();
            _subsidiaryService = Substitute.For<ISubsidiaryService>();
            _productService = Substitute.For<IProductService<UpsertSaleCommand>>();
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<SaleProfile>();
                cfg.AddProfile<WebApi.Features.Sales.Mapping.SaleProfile>();
            }).CreateMapper();


            _service = new(_userService, _subsidiaryService, _productService, _mapper);
        }

        [Fact]
        public async Task ValidateSale_WithoutDiscount_Async()
        {
            var command = new UpsertSaleCommand
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                CustomerId = Guid.NewGuid(),
                TotalAmount = 37,
                TotalAmountWithDiscount = 37,
                Canceled = false,
                SubsidiaryId = Guid.NewGuid(),
                Products = new[]
                                {
                                    new UpsertSaleProductCommand
                                    {

                                        ProductId = Guid.NewGuid(),
                                        Quantity = 4,
                                        Price = 9.25M,
                                        TotalAmount = 37,
                                        TotalAmountWithDiscount = 37,
                                        Discount = 0
                                    }
                                }
            };

            SaleProduct[] products = new[]
                                         {
                                             new SaleProduct
                                             {

                                                 ProductId = Guid.NewGuid(),
                                                 Quantity = 4,
                                                 Price = 9.25M,
                                                 Discount = 0
                                             }
                                        };

            _userService.GetAndValidateUser(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(new User());
            _subsidiaryService.GetAndValidateSubsidiary(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(new Subsidiary());
            _productService.GetAndValidateUpsertSaleProducts(Arg.Any<UpsertSaleCommand>(), Arg.Any<CancellationToken>()).Returns(products);

            var result = await _service.ValidateUpsertSaleAsync(command, new CancellationToken(false));

            Assert.Equal(command.TotalAmount, result.TotalAmount);
            Assert.Equal(command.TotalAmountWithDiscount, result.TotalAmountWithDiscount);
        }

        [Fact]
        public async Task ValidateSale_With20PercentDiscount_Async()
        {
            var command = new UpsertSaleCommand
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                CustomerId = Guid.NewGuid(),
                TotalAmount = 92.50M,
                TotalAmountWithDiscount = 74.00M,
                Canceled = false,
                SubsidiaryId = Guid.NewGuid(),
                Products = new[]
                                {
                                    new UpsertSaleProductCommand
                                    {

                                        ProductId = Guid.NewGuid(),
                                        Quantity = 10,
                                        Price = 9.25M,
                                        TotalAmount = 92.50M,
                                        TotalAmountWithDiscount = 74.00M,
                                        Discount = 20
                                    }
                                }
            };

            SaleProduct[] products = new[]
                                         {
                                             new SaleProduct
                                             {

                                                 ProductId = Guid.NewGuid(),
                                                 Quantity = 10,
                                                 Price = 9.25M,
                                                 Discount = 20
                                             }
                                        };

            _userService.GetAndValidateUser(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(new User());
            _subsidiaryService.GetAndValidateSubsidiary(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(new Subsidiary());
            _productService.GetAndValidateUpsertSaleProducts(Arg.Any<UpsertSaleCommand>(), Arg.Any<CancellationToken>()).Returns(products);

            var result = await _service.ValidateUpsertSaleAsync(command, new CancellationToken(false));
            decimal expectedValue = MathOperations.RoundDecimalWithTwoPlaces(result.TotalAmount - (result.TotalAmount * 0.2M));

            Assert.NotEqual(result.TotalAmount, result.TotalAmountWithDiscount);
            Assert.True(result.TotalAmount > result.TotalAmountWithDiscount);
            Assert.Equal(expectedValue, result.TotalAmountWithDiscount);
        }

        [Fact]
        public async Task ValidateSale_With10PercentDiscount_Async()
        {
            var command = new UpsertSaleCommand
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                CustomerId = Guid.NewGuid(),
                TotalAmount = 83.25M,
                TotalAmountWithDiscount = 74.93M,
                Canceled = false,
                SubsidiaryId = Guid.NewGuid(),
                Products = new[]
                                {
                                    new UpsertSaleProductCommand
                                    {

                                        ProductId = Guid.NewGuid(),
                                        Quantity = 9,
                                        Price = 9.25M,
                                        TotalAmount = 83.25M,
                                        TotalAmountWithDiscount = 74.93M,
                                        Discount = 10
                                    }
                                }
            };

            SaleProduct[] products = new[]
                                         {
                                             new SaleProduct
                                             {

                                                 ProductId = Guid.NewGuid(),
                                                 Quantity = 9,
                                                 Price = 9.25M,
                                                 Discount = 10
                                             }
                                        };

            _userService.GetAndValidateUser(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(new User());
            _subsidiaryService.GetAndValidateSubsidiary(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(new Subsidiary());
            _productService.GetAndValidateUpsertSaleProducts(Arg.Any<UpsertSaleCommand>(), Arg.Any<CancellationToken>()).Returns(products);

            var result = await _service.ValidateUpsertSaleAsync(command, new CancellationToken(false));
            decimal expectedValue = MathOperations.RoundDecimalWithTwoPlaces(result.TotalAmount - (result.TotalAmount * 0.1M));

            Assert.NotEqual(result.TotalAmount, result.TotalAmountWithDiscount);
            Assert.True(result.TotalAmount > result.TotalAmountWithDiscount);
            Assert.Equal(expectedValue, result.TotalAmountWithDiscount);
        }
    }
}
