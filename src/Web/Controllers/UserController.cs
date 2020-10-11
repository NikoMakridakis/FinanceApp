using AutoMapper;
using Core.Entities;
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
        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<UserController> logger, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> Login(UserForLoginDto userForLoginDto)
        {
            string email = userForLoginDto.Email;
            User user = await _userManager.FindByEmailAsync(email);

            if (user == null)   
            {
                return NotFound($"Unable to find user with email '{email}'.");
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, userForLoginDto.Password, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme,
                    new ClaimsPrincipal(identity));
                
                _logger.LogInformation("The user successfully logged in.");

                return RedirectToAction(nameof(BudgetGroupController.GetBudgetGroups), "Home");
            }

            if (result.IsLockedOut)
            {
                _logger.LogInformation("The user has been locked out.");

                int lockoutMinutesRemaining = user.GetLockoutMinutesRemaining();

                return Unauthorized($"The user '{email}' has been locked out. Please try again in {lockoutMinutesRemaining} minutes.");
            }

            if (!result.Succeeded)
            {
                return Unauthorized("The login attempt failed.");
            }

            return Ok(_mapper.Map<UserDto>(user));
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> Register(UserForRegisterDto userForRegisterDto)
        {
            string email = userForRegisterDto.Email;
            User user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                return BadRequest($"The email '{email}' has already been used.");
            }

            user = _mapper.Map<User>(userForRegisterDto);

            IdentityResult result = await _userManager.CreateAsync(user, userForRegisterDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return Ok(_mapper.Map<UserDto>(user));
        }
    }
}