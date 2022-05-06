using Api.Models;
using AutoMapper;
using Domain;

namespace Api;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<RaceModel, Race>().ReverseMap();
        CreateMap<RaceHorseModel, RaceHorse>().ReverseMap();
        CreateMap<UserBetModel, UserBet>().ReverseMap();
        CreateMap<UserModel, User>().ReverseMap();
    }
}
