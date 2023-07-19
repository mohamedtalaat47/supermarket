using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using supermarket.Dto;
using supermarket.Dto.UserDTOs;
using supermarket.Models;

namespace supermarket.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> Register(User user);
        Task<bool> EmailExists(string email);
        Task<ICollection<UserDto>> GetUsers();
        Task<ICollection<UserDto>> GetUsersManual();

        Task<ICollection<UserDtoCustom>> GetUsersManualCustomNames();

    }
}