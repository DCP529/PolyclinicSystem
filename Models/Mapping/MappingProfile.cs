using AutoMapper;
using Models.ModelsDb;

namespace Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<City, CityDb>().ReverseMap();

            CreateMap<Account, AccountDb>().ReverseMap();

            CreateMap<Role, RoleDb>().ReverseMap();

            CreateMap<Login, LoginDb>().ReverseMap();

            CreateMap<Specialization, SpecializationDb>().ReverseMap();

            CreateMap<DoctorDb, Doctor>().ReverseMap()
                .ForMember(x => x.ImagePath, x => x.MapFrom(x =>
                $"C:\\Users\\37377\\source\\repos\\PolyclinicSystem\\PolyclinicSystem\\wwwroot\\Images\\{x.Image.FileName}"));

            CreateMap<PolyclinicDb, Polyclinic>().ReverseMap()
                .ForMember(x => x.ImagePath, x => x.MapFrom(x =>
                                $"C:\\Users\\37377\\source\\repos\\PolyclinicSystem\\PolyclinicSystem\\wwwroot\\Images\\{x.Image.FileName}"));
        }
    }
}
