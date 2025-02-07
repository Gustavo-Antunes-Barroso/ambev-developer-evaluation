using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
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
    public class UpdateSaleHandlerTest
    {
        private readonly UpdateSaleHandler _handler;
        private readonly ISaleRepository _saleRepository;
        private readonly ISaleProductRepository _saleProductRepository;
        private readonly IValidateUpsertSaleService<UpsertSaleCommand> _validateUpsertSaleService;
        private readonly IMapper _mapper;

        private UpdateSaleCommand command;
        private Sale sale;
        public UpdateSaleHandlerTest()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _saleProductRepository = Substitute.For<ISaleProductRepository>();
            _validateUpsertSaleService = Substitute.For<IValidateUpsertSaleService<UpsertSaleCommand>>();
            _mapper = Substitute.For<IMapper>();

            _handler = new UpdateSaleHandler(_saleRepository, _saleProductRepository, _validateUpsertSaleService, _mapper);

            command = UpdateSaleCommandData.GenerateValidRandomUpdateSaleCommand();
            sale = SaleEntityData.GenerateRandomSaleData();
            _mapper.Map<UpsertSaleCommand>(command).Returns(UpsertSaleCommandData.GenerateValidRandomUpsertSaleCommand());
            _mapper.Map<UpsertSaleResult>(Arg.Any<Sale>()).Returns(UpsertSaleResultData.GenerateRandomUpsertSaleResult());
            _saleRepository.UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(true);
            _saleProductRepository.DeleteBySaleIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _saleProductRepository.CreateManyAsync(Arg.Any<SaleProduct[]>(), Arg.Any<CancellationToken>()).Returns(new List<SaleProduct>().ToArray());
            _saleRepository.MongoDbUpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(true);
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
                .Returns(Task.FromException<Sale>(new InvalidOperationException($"Sale total amount {command.TotalAmount} is wrong!")));
            
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => _handler.Handle(command, new CancellationToken(false)));
        }
    }
}
