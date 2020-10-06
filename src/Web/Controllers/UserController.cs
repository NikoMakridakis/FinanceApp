using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            string email = userLoginDto.Email;
            User userByEmail = await _userManager.FindByEmailAsync(email);

            if (userByEmail == null)
            {
                return NotFound($"Unable to find user with the email '{email}'.");
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(userByEmail, userLoginDto.Password, true);

            if (result.Succeeded)
            {
                _logger.LogInformation($"The user successfully logged in.");
            }

            if (!result.Succeeded)
            {
                return Unauthorized("The login information is incorrect.");
            }

            return new UserDto
            {
                Email = userByEmail.Email,
                UserName = userByEmail.UserName,
                Token = "This will be a token",
                MonthlyIncome = userByEmail.MonthlyIncome,
                FirstName = userByEmail.FirstName,
                LastName = userByEmail.LastName,
                BudgetGroups = userByEmail.BudgetGroups
            };
        }


        
    }
}