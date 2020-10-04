using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMapper _mapper;
        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(UserLoginDto userLoginDto)
        {
            string email = userLoginDto.Email;
            User user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound($"Unable to find user with email '{email}'.");
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, userLoginDto.Password, true);

            if (!result.Succeeded)
            {
                return Unauthorized("The username or password is incorrect.");
            }

            return new UserDto
            {
                Email = user.Email,
                UserName = user.UserName,
                Token = "This will be a token",
                MonthlyIncome = user.MonthlyIncome,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BudgetGroups = user.BudgetGroups
            };
        }


        
    }
}