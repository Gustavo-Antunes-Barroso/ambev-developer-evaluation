using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Shared.Commands;
using Ambev.DeveloperEvaluation.Application.Shared.Results;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpsertSale;
using AutoMapper;
using static Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale.UpdateSaleResponse;
using static Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpsertSale.UpsertSaleResponse;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleProfile : Profile
    {
        public UpdateSaleProfile()
        {
            CreateMap<UpdateSaleRequest, UpdateSaleCommand>();
            CreateMap<UpdateSaleProductsRequest, UpdateSaleProductCommand>();
            CreateMap<UpdateSaleCommand, UpsertSaleCommand>();
            CreateMap<UpdateSaleProductCommand, UpsertSaleProductCommand>();

            CreateMap<UpsertSaleResult, UpdateSaleResponse>();
            CreateMap<UpsertSaleProductResult, UpdateSaleProductResponse>();
            CreateMap<UpsertSaleSubsidiaryResult, UpdateSaleSubsidiaryResponse>();
            CreateMap<UpsertSaleCustomerResult, UpdateSaleCustomerResponse>();

            CreateMap<UpsertSaleResult, UpsertSaleResponse>();
            CreateMap<UpsertSaleProductResult, UpsertSaleProductResponse>();
            CreateMap<UpsertSaleSubsidiaryResult, UpsertSaleSubsidiaryResponse>();
            CreateMap<UpsertSaleCustomerResult, UpsertSaleCustomerResponse>();
        }
    }
}
