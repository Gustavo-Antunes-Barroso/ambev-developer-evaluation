using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using AutoMapper;
using static Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale.CreateSaleResponse;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Mapping
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            #region<<<RequestToCommand>>>
            CreateMap<CreateSaleRequest, CreateSaleCommand>();
            CreateMap<CreateSaleProductsRequest, CreateSaleProductsCommand>();
            CreateMap<DeleteSaleRequest, DeleteSaleCommand>();
            CreateMap<GetSaleRequest, GetSaleCommand>();
            #endregion

            #region<<<ResultToResponse>>>
            CreateMap<CreateSaleResult, CreateSaleResponse>();
            CreateMap<CreateSaleProductResult, CreateSaleProductResponse>();
            CreateMap<CreateSaleSubsidiaryResult, CreateSaleSubsidiaryResponse>();
            CreateMap<CreateSaleCustomerResult, CreateSaleCustomerResponse>();

            CreateMap<GetSaleResult, GetSaleResponse>();
            CreateMap<GetSaleProductResult, GetSaleProductResponse>();
            CreateMap<GetSaleSubsidiaryResult, GetSaleSubsidiaryResponse>();
            CreateMap<GetSaleCustomerResult, GetSaleCustomerResponse>();
            #endregion
        }
    }
}
