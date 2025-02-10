using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Shared.Commands;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleRequest, CreateSaleCommand>();

            CreateMap<CreateSaleCommand, UpsertSaleCommand>();
            CreateMap<CreateSaleProductCommand, UpsertSaleProductCommand>();
            CreateMap<CreateSaleProductsRequest, CreateSaleProductCommand>();
        }
    }
}
