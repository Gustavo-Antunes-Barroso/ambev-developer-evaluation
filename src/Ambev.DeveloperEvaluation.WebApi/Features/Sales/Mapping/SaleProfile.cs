using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using AutoMapper;
using static Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale.CreateSaleResponse;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Mapping
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<CreateSaleRequest, CreateSaleCommand>();
            CreateMap<CreateSaleProductsRequest, CreateSaleProductsCommand>();
            CreateMap<Sale, CreateSaleResult>();

            CreateMap<SaleProduct, CreateSaleProductResult>();
            CreateMap<SaleSubsidiary, CreateSaleSubsidiaryResult>();
            CreateMap<SaleCustomer, CreateSaleCustomerResult>();

            CreateMap<CreateSaleResult, CreateSaleResponse>();
            CreateMap<CreateSaleProductResult, CreateSaleProductResponse>();
            CreateMap<CreateSaleSubsidiaryResult, CreateSaleSubsidiaryResponse>();
            CreateMap<CreateSaleCustomerResult, CreateSaleCustomerResponse>();
        }
    }
}
