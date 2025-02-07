using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Shared.Commands;
using Ambev.DeveloperEvaluation.Application.Shared.Results;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpsertSale;
using AutoMapper;
using static Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale.UpdateSaleResponse;
using static Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpsertSale.UpsertSaleResponse;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Mapping
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            #region<<<RequestToCommand>>>
            //Create
            CreateMap<CreateSaleRequest, CreateSaleCommand>();
            CreateMap<CreateSaleCommand, UpsertSaleCommand>();
            CreateMap<CreateSaleProductCommand, UpsertSaleProductCommand>();
            CreateMap<CreateSaleProductsRequest, CreateSaleProductCommand>();

            //Update
            CreateMap<UpdateSaleRequest, UpdateSaleCommand>();
            CreateMap<UpdateSaleProductsRequest, UpdateSaleProductCommand>();
            CreateMap<UpdateSaleCommand, UpsertSaleCommand>();
            CreateMap<UpdateSaleProductCommand, UpsertSaleProductCommand>();

            CreateMap<UpsertSaleResult, UpdateSaleResponse>();
            CreateMap<UpsertSaleProductResult, UpdateSaleProductResponse>();
            CreateMap<UpsertSaleSubsidiaryResult, UpdateSaleSubsidiaryResponse>();
            CreateMap<UpsertSaleCustomerResult, UpdateSaleCustomerResponse>();
            //Delete
            CreateMap<DeleteSaleRequest, DeleteSaleCommand>();

            //Get
            CreateMap<GetSaleRequest, GetSaleCommand>();
            #endregion

            #region<<<ResultToResponse>>>
            //Upsert
            CreateMap<UpsertSaleResult, UpsertSaleResponse>();
            CreateMap<UpsertSaleProductResult, UpsertSaleProductResponse>();
            CreateMap<UpsertSaleSubsidiaryResult, UpsertSaleSubsidiaryResponse>();
            CreateMap<UpsertSaleCustomerResult, UpsertSaleCustomerResponse>();

            //Get
            CreateMap<GetSaleResult, GetSaleResponse>();
            CreateMap<GetSaleProductResult, GetSaleProductResponse>();
            CreateMap<GetSaleSubsidiaryResult, GetSaleSubsidiaryResponse>();
            CreateMap<GetSaleCustomerResult, GetSaleCustomerResponse>();
            #endregion
        }
    }
}
