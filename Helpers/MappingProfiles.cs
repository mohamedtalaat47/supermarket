using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using supermarket.Dto.UserDTOs;
using supermarket.Models;

namespace supermarket.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserRegistrationDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}