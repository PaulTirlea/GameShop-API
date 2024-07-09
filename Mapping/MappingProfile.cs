using AutoMapper;
using SummerPracticePaul.Models;
using SummerPracticePaul.Repository.Dto;

namespace SummerPracticePaul.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Game, GameDto>().ReverseMap();
            CreateMap<GameCategory, GameCategoryDto>().ReverseMap();
            CreateMap<UserRegistrationDto, User>();
            CreateMap<UserRegistrationDto, UserDto>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
            CreateMap<UserDto, User>();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Discount, DiscountDto>().ReverseMap();
            CreateMap<Review, ReviewDto>().ReverseMap();

        }
    }
}
