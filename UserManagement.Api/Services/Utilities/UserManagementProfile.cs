using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using UserManagement.Api.Domain;
using UserManagement.Api.Domain.Dtos;

namespace UserManagement.Api.Services.Utilities
{
    public class UserManagementProfile : Profile
    {
        public UserManagementProfile()
        {
            CreateMap<Class, ClassDto>().ReverseMap();
            CreateMap<Users, RegistrationDto>().ReverseMap();
            CreateMap<Users, LoginDto>().ReverseMap();
            CreateMap<Users, UserResponseDto>().ReverseMap();
            CreateMap<Users, RegistrationResponseDto>().ReverseMap();
        }
    }
}