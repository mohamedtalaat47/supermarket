using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using supermarket.Interfaces;
using supermarket.Models;
using supermarket.Helpers;
using Microsoft.EntityFrameworkCore;
using supermarket.Dto;
using AutoMapper;
using supermarket.Dto.UserDTOs;

namespace supermarket.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SuperMarketContext _context;
        private readonly IMapper mapper;
        private readonly ManualMapper manMapper;

        public UserRepository(SuperMarketContext context, IMapper mapper, ManualMapper manualMapper)
        {
            this._context = context;
            this.mapper = mapper;
            this.manMapper = manualMapper;
        }
        public async Task<bool> Register(User user)
        {
            try
            {
                await _context.AddAsync(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email)) != null;
        }

        public async Task<ICollection<UserDto>> GetUsers()
        {
            return await _context.Users.Select(u => mapper.Map<UserDto>(u)).ToListAsync();
        }

        public async Task<ICollection<UserDto>> GetUsersManual()
        {
            var users = await _context.Users.ToListAsync();
            var usersDto = manMapper.MapManuallyList<User, UserDto>(users);
            return usersDto;
        }

        public async Task<ICollection<UserDtoCustom>> GetUsersManualCustomNames()
        {
            var propMap = new Dictionary<string, string> {
                { "Username", "NewName" },
                };
            var users = await _context.Users.ToListAsync();
            var usersDto = manMapper.MapManuallyListCustomNames<User, UserDtoCustom>(users, propMap);
            return usersDto;
        }
    }
}