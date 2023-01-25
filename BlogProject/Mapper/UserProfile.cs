using AutoMapper;
using BlogProject.Models.API.Request;
using BlogProject.Models.Database.Users;
using BlogProject.Models.ViewModels;

namespace BlogProject.Mapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterRequest, User>()
            .ForMember(x => x.Email, opt => opt.MapFrom(c => c.EmailReg))
            .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Login));
    }
}