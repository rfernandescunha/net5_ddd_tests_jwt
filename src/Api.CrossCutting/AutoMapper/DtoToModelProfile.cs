using AutoMapper;
using Api.Domain.Dto.User;
using Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace crosscutting.AutoMapper
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<UserModel, UserDtoCreate>().ReverseMap();
            CreateMap<UserModel, UserDtoUpdate>().ReverseMap();
            CreateMap<UserModel, UserDtoFind>().ReverseMap();
        }
    }
}
