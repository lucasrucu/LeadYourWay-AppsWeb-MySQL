using AutoMapper;
using LeadYourWay.API.Response;
using LeadYourWay.Infrastructure.Models;

namespace LeadYourWay.API.Mapper;

public class ModelToResponse : Profile
{
    public ModelToResponse()
    {
        CreateMap<User, UserResponse>();
    }
}