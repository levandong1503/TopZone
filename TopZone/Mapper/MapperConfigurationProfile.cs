using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using TopZone.Dtos;
using Type = Domain.Entities.Type;
namespace TopZone.Mapper
{
    public class MapperConfigurationProfile : Profile
    {
        public MapperConfigurationProfile()
        {
            AllowNullCollections = true;
            
            CreateMap<TypeRequest, Type>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.MainTypeId, opt => opt.MapFrom(src => src.MainTypeId));

            CreateMap<ProductDto, Product>();

            CreateMap<ProductRequest, Product>();
        }
    }
}
