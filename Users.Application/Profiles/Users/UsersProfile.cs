using AutoMapper;
using Users.Application.Dtos.Users;
using Users.Domain.Models;

namespace Users.Application.Profiles.Users
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserReadDto>();
            CreateMap<UserReadDto, User>();
            CreateMap<UserRegisterDto, User>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TempId));
            CreateMap<UserUpdateDto, User>();   
        }
    }
}
