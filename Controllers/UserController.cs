using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using supermarket.Dto;
using supermarket.Helpers;
using supermarket.Interfaces;
using supermarket.Models;
using supermarket.Dto.UserDTOs;

namespace supermarket.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly ManualMapper _manMapper;

        public UserController(IUserRepository repository, IMapper mapper, ManualMapper manMapper)
        {
            _repository = repository;
            _mapper = mapper;
            _manMapper = manMapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto user)
        {
            if (await _repository.EmailExists(user.Email))
                return BadRequest("User already exists");

            if (user.Password != user.ConfirmPassword)
                throw new InvalidDataException("Passwords don't match");

            var UserMap = _mapper.Map<User>(user);
            UserMap.Role = "User";

            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            UserMap.Password = BCrypt.Net.BCrypt.HashPassword(UserMap.Password, salt);

            if (!await _repository.Register(UserMap))
            {
                return BadRequest(ModelState);
            }

            return Created("User registered succefully", User);
        }

        [HttpPost("registerManual")]
        public async Task<IActionResult> registerManual([FromBody] UserRegistrationDto user)
        {
            if (await _repository.EmailExists(user.Email))
                return BadRequest("User already exists");

            if (user.Password != user.ConfirmPassword)
                throw new InvalidDataException("Passwords don't match");

            string salt = BCrypt.Net.BCrypt.GenerateSalt();

            var UserMap = new User() { Username = user.Username, Email = user.Email, Password = BCrypt.Net.BCrypt.HashPassword(user.Password, salt), Role = "User" };

            if (!await _repository.Register(UserMap))
            {
                return BadRequest(ModelState);
            }

            return Created("User registered succefully", User);
        }

        [HttpPost("registerMapper")]
        public async Task<IActionResult> RegisterMapper([FromBody] UserRegistrationDto user)
        {
            if (await _repository.EmailExists(user.Email))
                return BadRequest("User already exists");

            if (user.Password != user.ConfirmPassword)
                throw new InvalidDataException("Passwords don't match");

            var UserMap = _manMapper.MapManually<UserRegistrationDto, User>(user);
            UserMap.Role = "User";

            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            UserMap.Password = BCrypt.Net.BCrypt.HashPassword(UserMap.Password, salt);

            if (!await _repository.Register(UserMap))
            {
                return BadRequest(ModelState);
            }

            return Created("User registered succefully", User);
        }


        [HttpPost("test")]
        public async Task<IActionResult> TestAsync()
        {
            var t1 = Task.Delay(2000);
            var t2 = Task.Delay(3000);
            var t3 = Task.Delay(5000);
            await t1;
            await t2;
            await t3;
            return Ok(new { T1 = t1.IsCompleted, T2 = t2.IsCompleted, T3 = t3.IsCompleted });
        }



        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
            return Ok(await _repository.GetUsers());
        }

        [HttpGet("indexManual")]
        public async Task<IActionResult> IndexManual()
        {
            return Ok(await _repository.GetUsersManual());
        }

                [HttpGet("indexManualCustomNames")]
        public async Task<IActionResult> IndexManualCustomNames()
        {
            return Ok(await _repository.GetUsersManualCustomNames());
        }
    }
}