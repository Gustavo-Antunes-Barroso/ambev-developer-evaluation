using Ambev.DeveloperEvaluation.Application.Shared.Commands;
using Ambev.DeveloperEvaluation.Application.Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            CreateMap<UpsertSaleCommand, Sale>();
            CreateMap<UpsertSaleProductCommand, SaleProduct>();


            CreateMap<Sale, UpsertSaleResult>();
            CreateMap<SaleProduct, UpsertSaleProductResult>();
            CreateMap<SaleSubsidiary, UpsertSaleSubsidiaryResult>();
            CreateMap<SaleCustomer, UpsertSaleCustomerResult>();
        }
    }
}
