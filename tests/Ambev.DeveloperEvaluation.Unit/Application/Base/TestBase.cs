using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Shared.Commands;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using NSubstitute;

namespace Ambev.DeveloperEvaluation.Unit.Application.Base
{
    public class TestBase
    {
        //Services
        public readonly IUserService _userService;
        public readonly ISubsidiaryService _subsidiaryService;
        public readonly IProductService<UpsertSaleCommand> _productService;

        //Repositories
        public readonly ISaleRepository _saleRepository;
        public readonly ISaleProductRepository _saleProductRepository;
        public readonly IProductRepository _productRepository;
        public readonly IValidateUpsertSaleService<UpsertSaleCommand> _validateUpsertSaleService;
        public readonly ISubsidiaryRepository _subsidiaryRepository;
        public readonly IUserRepository _userRepository;
        public readonly IRabbitMQProducer<Sale> _rabbitMQProducer;

        //Mapper
        public readonly IMapper _mapper;

        //CancellationToken
        public readonly CancellationToken cancellationToken;

        public TestBase()
        {
            //Services
            _userService = Substitute.For<IUserService>();
            _subsidiaryService = Substitute.For<ISubsidiaryService>();
            _productService = Substitute.For<IProductService<UpsertSaleCommand>>();

            //Repositories
            _saleRepository = Substitute.For<ISaleRepository>();
            _saleProductRepository = Substitute.For<ISaleProductRepository>();
            _productRepository = Substitute.For<IProductRepository>();
            _validateUpsertSaleService = Substitute.For<IValidateUpsertSaleService<UpsertSaleCommand>>();
            _subsidiaryRepository = Substitute.For<ISubsidiaryRepository>();
            _userRepository = Substitute.For<IUserRepository>();
            _rabbitMQProducer = Substitute.For<IRabbitMQProducer<Sale>>();
            _rabbitMQProducer.SendMessage(Arg.Any<Sale>(), Arg.Any<string>()).Returns(true);
            //Mapper
            _mapper = new MapperConfiguration(cfg => 
            { 
                //Application
                cfg.AddProfile<CreateSaleProfile>();
                cfg.AddProfile<UpdateSaleProfile>();
                cfg.AddProfile<GetSaleProfile>();

                //Api
                cfg.AddProfile<WebApi.Features.Sales.CreateSale.CreateSaleProfile>();
                cfg.AddProfile<WebApi.Features.Sales.DeleteSale.DeleteSaleProfile>();
                cfg.AddProfile<WebApi.Features.Sales.UpdateSale.UpdateSaleProfile>();
                cfg.AddProfile<WebApi.Features.Sales.GetSale.GetSaleProfile>();
            }).CreateMapper();

            //CancellationToken
            cancellationToken = new CancellationToken(false);
        }
    }
}
