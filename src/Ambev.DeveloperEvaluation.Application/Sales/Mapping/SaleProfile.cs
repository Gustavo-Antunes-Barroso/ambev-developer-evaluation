using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Shared.Commands;
using Ambev.DeveloperEvaluation.Application.Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.Mapping
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<UpsertSaleCommand, Sale>()
                .ForMember(x => x.Products, opt => opt.Ignore());
            CreateMap<User, SaleCustomer>();
            CreateMap<Subsidiary, SaleSubsidiary>();
            CreateMap<Product, SaleProduct>()
                .ForMember(x => x.ProductId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Id, opt => opt.Ignore());

            #region<<<EntityToResult>>>
            CreateMap<Sale, UpsertSaleResult>();
            CreateMap<SaleProduct, UpsertSaleProductResult>();
            CreateMap<SaleSubsidiary, UpsertSaleSubsidiaryResult>();
            CreateMap<SaleCustomer, UpsertSaleCustomerResult>();

            CreateMap<Sale, GetSaleResult>();
            CreateMap<SaleProduct, GetSaleProductResult>();
            CreateMap<SaleSubsidiary, GetSaleSubsidiaryResult>();
            CreateMap<SaleCustomer, GetSaleCustomerResult>();
            #endregion
        }
    }
}
