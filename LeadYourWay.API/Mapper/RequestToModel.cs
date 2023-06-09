using AutoMapper;
using LeadYourWay.API.Request;
using LeadYourWay.Infrastructure.Models;

namespace LeadYourWay.API.Mapper;

public class RequestToModel : Profile
{
    public RequestToModel()
    {
        CreateMap<UserRequest, User>();
        CreateMap<LoginRequest, User>();
        CreateMap<CardRequest, Card>();
        CreateMap<BicycleRequest, Bicycle>();
    }
}