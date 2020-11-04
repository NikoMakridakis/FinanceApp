using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Web.Extensions;
using Web.Models;
using Web.Services.EmailService;
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
        private readonly IEmailSender _emailSender;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<UserController> logger,
            IMapper mapper, ITokenService tokenService, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;
            _tokenService = tokenService;
            _emailSender = emailSender;
        }

        

        // POST: api/user/login
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> Login(UserForLoginDto userForLoginDto)
        {
            string email = userForLoginDto.Email;
            User user = await _userManager.FindByEmailAsync(email);

            if (user == null)   
            {
                string error = $"Unable to find user with the email '{email}'.";
                _logger.LogError(error);
                return NotFound(error);
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, userForLoginDto.Password, lockoutOnFailure: true);

            if (result.IsLockedOut)
            {
                int lockoutSecondsRemaining = user.GetLockoutSecondsRemaining();
                string error = $"The user with the email '{email}' has been locked out for {lockoutSecondsRemaining} seconds.";
                _logger.LogInformation(error);

                UserDto userLockedOut = new UserDto
                {
                    Email = user.Email,
                    IsLockedOut = true,
                    LockoutSeconds = lockoutSecondsRemaining
                };

                return Unauthorized(userLockedOut);
            }

            if (!result.Succeeded)
            {
                string error = $"The user with the email '{email}' has failed the login attempt.";
                _logger.LogInformation(error);
                return Unauthorized(error);
            }

            _logger.LogInformation("The user successfully logged in.");

            return new UserDto
            {
                Email = user.Email,
                AccessToken = _tokenService.CreateToken(user),
            };
        }

        // POST: api/user/register
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> Register(UserForRegisterDto userForRegisterDto)
        {
            string email = userForRegisterDto.Email;
            User user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                _logger.LogError($"The email '{email}' has already been registered to a different user.");
                return Unauthorized();
            }

            user = _mapper.Map<User>(userForRegisterDto);

            IdentityResult result = await _userManager.CreateAsync(user, userForRegisterDto.Password);

            if (!result.Succeeded)
            {
                _logger.LogError($"The user with the email '{email}' has failed the registration attempt.");
                return BadRequest();
            }

            return new UserDto
            {
                Email = user.Email,
                FullName = user.FullName,
                AccessToken = _tokenService.CreateToken(user)
            };
        }

        // POST: api/user/forgotPassword
        [HttpPost("forgotPassword")]
        public async Task<ActionResult> ForgotPassword(UserForgotPasswordDto userForgotPasswordDto)
        {
            string email = userForgotPasswordDto.Email;
            User user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                string error = $"Unable to find user with the email '{email}'.";
                _logger.LogError(error);
                return NotFound(error);
            }

            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            UserResetPasswordDto userResetPasswordDto = new UserResetPasswordDto
            {
                Email = user.Email,
                ResetToken = resetToken
            };

            string callback = Url.Action(nameof(ResetPassword), "api/user", userResetPasswordDto, Request.Scheme);
            callback = callback.Replace("%2F", "/");

            var message = new Message(new string[] { user.Email }, "Reset password token", callback);
            await _emailSender.SendEmailAsync(message);

            return Ok(userResetPasswordDto);
        }

        // POST: api/user/resetPassword
        [HttpPost("resetPassword")]
        public async Task<ActionResult> ResetPassword(UserResetPasswordDto userResetPasswordDto)
        {
            string email = userResetPasswordDto.Email;
            User user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                string error = $"Unable to find user with the email '{email}'.";
                _logger.LogError(error);
                return NotFound(error);
            }

            IdentityResult resetPassResult = await _userManager.ResetPasswordAsync(user, userResetPasswordDto.ResetToken, userResetPasswordDto.Password);
            if (!resetPassResult.Succeeded)
            {
                string error = $"Failed to reset the password associated with the email '{email}'.";
                _logger.LogError(error);
                return BadRequest(error);
            }

            return Ok();
        }
    }
}