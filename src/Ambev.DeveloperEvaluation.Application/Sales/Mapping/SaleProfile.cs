using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.Mapping
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<CreateSaleCommand, Sale>()
                .ForMember(x => x.Products, opt => opt.Ignore());
            CreateMap<User, SaleCustomer>();
            CreateMap<Subsidiary, SaleSubsidiary>();
            CreateMap<Product, SaleProduct>()
                .ForMember(x => x.ProductId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Id, opt => opt.Ignore());

            #region<<<EntityToResult>>>
            CreateMap<Sale, CreateSaleResult>();
            CreateMap<SaleProduct, CreateSaleProductResult>();
            CreateMap<SaleSubsidiary, CreateSaleSubsidiaryResult>();
            CreateMap<SaleCustomer, CreateSaleCustomerResult>();

            CreateMap<Sale, GetSaleResult>();
            CreateMap<SaleProduct, GetSaleProductResult>();
            CreateMap<SaleSubsidiary, GetSaleSubsidiaryResult>();
            CreateMap<SaleCustomer, GetSaleCustomerResult>();
            #endregion
        }
    }
}
