using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleProfile : Profile
    {
        public UpdateSaleProfile()
        {
            CreateMap<User, SaleCustomer>();
            CreateMap<Subsidiary, SaleSubsidiary>();
            CreateMap<Product, SaleProduct>()
                .ForMember(x => x.ProductId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Id, opt => opt.Ignore()); ;
        }
    }
}
