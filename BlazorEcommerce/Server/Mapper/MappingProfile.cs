using AutoMapper;
using BlazorEcommerce.Shared.Models;

namespace BlazorEcommerce.Server.Mapper
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDto>().ForMember(t => t.ClassName, opt => opt.MapFrom(src => src.Class.Classname));
            CreateMap<PersonDto, Person>().ForMember(t => t.Class, opt=> opt.Ignore());



            CreateMap<ClassDto, IdValuePair>()
           .ForMember(t => t.Id, opt => opt.MapFrom(src => src.Classid))
           .ForMember(t => t.Value, opt => opt.MapFrom(src => src.Classname))
           .ReverseMap();
        }
    }
}
