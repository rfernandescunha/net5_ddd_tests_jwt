using Api.Domain.Entities;
using AutoMapper;
using Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace crosscutting.AutoMapper
{
    public class ModelToEntityProfile : Profile
    {
        public ModelToEntityProfile()
        {
            CreateMap<UserModel, User>().ReverseMap();
        }
    }
}
