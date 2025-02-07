using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Shared.Commands;
using Ambev.DeveloperEvaluation.Application.Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Unit.Application.SaleHandlersTest.Commands;
using Ambev.DeveloperEvaluation.Unit.Application.SaleHandlersTest.CommomData;
using Ambev.DeveloperEvaluation.Unit.Application.SaleHandlersTest.Result;
using AutoMapper;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleHandlersTest
{
    public class CreateSaleHandlerTest
    {
        private readonly CreateSaleHandler _handler;
        private readonly ISaleRepository _saleRepository;
        private readonly ISaleProductRepository _saleProductRepository;
        private readonly IValidateUpsertSaleService<UpsertSaleCommand> _validateUpsertSaleService;
        private readonly IMapper _mapper;

        private CreateSaleCommand command;
        private Sale sale;
        public CreateSaleHandlerTest()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _saleProductRepository = Substitute.For<ISaleProductRepository>();
            _validateUpsertSaleService = Substitute.For<IValidateUpsertSaleService<UpsertSaleCommand>>();
            _mapper = Substitute.For<IMapper>();

            _handler = new CreateSaleHandler(_saleRepository, _saleProductRepository, _validateUpsertSaleService, _mapper);

            command = CreateSaleCommandData.GenerateValidRandomSaleCommand();
            sale = SaleEntityData.GenerateRandomSaleData();
            _mapper.Map<UpsertSaleCommand>(command).Returns(UpsertSaleCommandData.GenerateValidRandomUpsertSaleCommand());
            _mapper.Map<UpsertSaleResult>(Arg.Any<Sale>()).Returns(UpsertSaleResultData.GenerateRandomUpsertSaleResult());
            _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(new Sale());
            _saleProductRepository.CreateManyAsync(Arg.Any<SaleProduct[]>(), Arg.Any<CancellationToken>()).Returns(new List<SaleProduct>().ToArray());
            _saleRepository.MongoDbCreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(new Sale());
        }

        [Fact]
        public async Task Handler_ValidCommand_ReturnSuccess_Async()
        {
            _validateUpsertSaleService.ValidateUpsertSaleAsync(Arg.Any<UpsertSaleCommand>(), Arg.Any<CancellationToken>()).Returns(sale);

            var result = await _handler.Handle(command, new CancellationToken(false));

            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.IsType<Guid>(result.Id);
        }

        [Fact]
        public async Task Handler_ValidCommand_ReturnsInvalidOperationException_Async()
        {
            _validateUpsertSaleService.ValidateUpsertSaleAsync(Arg.Any<UpsertSaleCommand>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromException<Sale>(new InvalidOperationException($"Sale total amount with discount {command.TotalAmountWithDiscount} is wrong!")));
           
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => _handler.Handle(command, new CancellationToken(false)));
        }
    }
}
