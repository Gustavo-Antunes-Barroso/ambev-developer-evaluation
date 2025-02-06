using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;
using AutoMapper;
using static Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale.CreateSaleResponse;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Mapping
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            //Create Sale
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

            //Delete Sale
            CreateMap<DeleteSaleRequest, DeleteSaleCommand>();
        }
    }
}
