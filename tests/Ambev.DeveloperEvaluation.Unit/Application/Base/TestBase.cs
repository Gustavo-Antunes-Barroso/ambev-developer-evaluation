using Ambev.DeveloperEvaluation.Application.Sales.Mapping;
using Ambev.DeveloperEvaluation.Application.Shared.Commands;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using NSubstitute;

namespace Ambev.DeveloperEvaluation.Unit.Application.Base
{
    public class TestBase
    {
        public readonly ISaleRepository _saleRepository;
        public readonly ISaleProductRepository _saleProductRepository;
        public readonly IProductRepository _productRepository;
        public readonly IValidateUpsertSaleService<UpsertSaleCommand> _validateUpsertSaleService;
        public readonly IMapper _mapper;

        public TestBase()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _saleProductRepository = Substitute.For<ISaleProductRepository>();
            _productRepository = Substitute.For<IProductRepository>();
            _validateUpsertSaleService = Substitute.For<IValidateUpsertSaleService<UpsertSaleCommand>>();
            _mapper = new MapperConfiguration(cfg => 
            { 
                cfg.AddProfile<SaleProfile>();
                cfg.AddProfile<WebApi.Features.Sales.Mapping.SaleProfile>();
            }).CreateMapper();
        }
    }
}
