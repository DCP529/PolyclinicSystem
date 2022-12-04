using AutoMapper;
using Models.ModelsDb;

namespace Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<City, CityDb>().ReverseMap();

            CreateMap<Specialization, SpecializationDb>().ReverseMap();

            CreateMap<DoctorDb, Doctor>().ReverseMap()
                .ForMember(x => x.ImagePath, x => x.MapFrom(x => x.Image.Name));

            CreateMap<PolyclinicDb, Polyclinic>().ReverseMap()
                .ForMember(x => x.ImagePath, x => x.MapFrom(x => x.Image.Name));
        }
    }
}
