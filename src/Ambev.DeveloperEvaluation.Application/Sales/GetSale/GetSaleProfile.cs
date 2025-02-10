using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleProfile : Profile
    {
        public GetSaleProfile()
        {
            CreateMap<Sale, GetSaleResult>();
            CreateMap<SaleProduct, GetSaleProductResult>();
            CreateMap<SaleSubsidiary, GetSaleSubsidiaryResult>();
            CreateMap<SaleCustomer, GetSaleCustomerResult>();
        }
    }
}
