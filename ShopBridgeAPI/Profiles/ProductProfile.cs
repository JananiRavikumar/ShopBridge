using AutoMapper;
using ShopBridgeAPI.Models;
using ShopBridgeAPI.Models.Dtos;

namespace ShopBridgeAPI.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductRequestDto, Product>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.ProductName));

            CreateMap<FetchProductResponseDto, Product>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.ProductId))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.ProductName));
        }
    }
}
