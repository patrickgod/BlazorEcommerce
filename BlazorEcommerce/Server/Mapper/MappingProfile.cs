using AutoMapper;
using BlazorEcommerce.Shared.Models;

namespace BlazorEcommerce.Server.Mapper
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDto>()
            .ForMember(t => t.ClassName, opt => opt.MapFrom(src => src.Class.Classname))
            .ReverseMap();
        }
    }
}
