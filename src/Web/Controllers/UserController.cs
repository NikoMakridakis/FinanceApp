﻿using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Extensions;
using Web.Models;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<UserController> logger,
            IMapper mapper, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> Login(UserForLoginDto userForLoginDto)
        {
            string email = userForLoginDto.Email;
            User user = await _userManager.FindByEmailAsync(email);

            if (user == null)   
            {
                _logger.LogError($"Unable to find user with the email '{email}'.");
                return NotFound();
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, userForLoginDto.Password, lockoutOnFailure: true);

            if (result.IsLockedOut)
            {
                int lockoutMinutesRemaining = user.GetLockoutMinutesRemaining();
                _logger.LogInformation($"The user with the email '{email}' has been locked out for {lockoutMinutesRemaining} minutes.");
                return Unauthorized();
            }

            if (!result.Succeeded)
            {
                _logger.LogInformation($"The user with the email '{email}' has failed the login attempt.");
                return Unauthorized();
            }

            _logger.LogInformation("The user successfully logged in.");

            return new UserDto {
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> Register(UserForRegisterDto userForRegisterDto)
        {
            string email = userForRegisterDto.Email;
            User user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                _logger.LogError($"The email '{email}' has already been registered to a different user.");
                return BadRequest();
            }

            user = _mapper.Map<User>(userForRegisterDto);

            IdentityResult result = await _userManager.CreateAsync(user, userForRegisterDto.Password);

            if (!result.Succeeded)
            {
                _logger.LogError($"The user with the email '{email}' has failed the registration attempt.");
                return BadRequest();
            }

            //return Ok(_mapper.Map<UserDto>(user));

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }
    }
}