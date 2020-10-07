using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
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
        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<UserController> logger, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(UserLoginDto userLoginDto)
        {
            string userName = userLoginDto.UserName;
            string email = userLoginDto.Email;
            User user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userName || u.Email == email);

            if (user == null)
            {
                return NotFound($"Unable to find user.");
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, userLoginDto.Password, true);

            if (result.Succeeded)
            {
                _logger.LogInformation($"The user successfully logged in.");
            }

            if (!result.Succeeded)
            {
                return Unauthorized("The login information is incorrect.");
            }

            return Ok(_mapper.Map<UserDto>(userLoginDto));
        }

            [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(UserRegisterDto userRegisterDto)
        {
            User user = _mapper.Map<User>(userRegisterDto);

            IdentityResult result = await _userManager.CreateAsync(user, userRegisterDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return Ok(_mapper.Map<UserDto>(user));
        }
    }
}