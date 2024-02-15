using Api.Domain.Dto.User;
using Api.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;


namespace crosscutting.AutoMapper
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<UserDtoCreate, User>().ReverseMap();
            CreateMap<UserDtoUpdate, User>().ReverseMap();
            CreateMap<UserDtoFind, User>().ReverseMap();

            CreateMap<UserDtoCreateResult, User>().ReverseMap();
            CreateMap<UserDtoUpdateResult, User>().ReverseMap();
            CreateMap<UserDtoFindResult, User>().ReverseMap();
        }
    }
}
