using AutoMapper;
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
using Web.Services.EmailService;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Web.Controllers
{
    [Route("[controller]")]
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

        // POST: user/login
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(UserForLoginDto userForLoginDto)
        {
            string email = userForLoginDto.Email;
            User user = await _userManager.FindByEmailAsync(email);

            if (user == null)   
            {
                string errorMessage = $"Unable to find user with the email: '{email}'.";
                _logger.LogError(errorMessage);
                return NotFound(errorMessage);
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, userForLoginDto.Password, lockoutOnFailure: true);

            if (result.IsLockedOut)
            {
                int lockoutSecondsRemaining = user.GetLockoutSecondsRemaining();
                string errorMessage = $"Failed to login the user with the email: '{email}'. The user has been locked out for {lockoutSecondsRemaining} seconds.";
                _logger.LogInformation(errorMessage);

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
                string errorMessage = $"Failed to login the user with the email: '{email}'.";
                _logger.LogInformation(errorMessage);
                return Unauthorized(errorMessage);
            }

            _logger.LogInformation($"Successful login for the user with the email: {email}.");

            return new UserDto
            {
                Email = user.Email,
                AccessToken = _tokenService.CreateToken(user),
            };
        }

        // POST: user/register
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(UserForRegisterDto userForRegisterDto)
        {
            string email = userForRegisterDto.Email;
            User user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                string errorMessage = $"Failed to register a new user with the email: '{email}'. This email has already been registered to a different user.";
                _logger.LogError(errorMessage);
                return Unauthorized(errorMessage);
            }

            user = _mapper.Map<User>(userForRegisterDto);

            IdentityResult result = await _userManager.CreateAsync(user, userForRegisterDto.Password);

            if (!result.Succeeded)
            {
                string errorMessage = $"Failed to register a new user with the email: '{email}'.";
                _logger.LogError(errorMessage);
                return BadRequest(errorMessage);
            }

            _logger.LogInformation($"Successfully registered a new user the the email: '{email}'.");

            return new UserDto
            {
                Email = user.Email,
                FullName = user.FullName,
                AccessToken = _tokenService.CreateToken(user)
            };
        }

        // POST: user/forgotPassword
        [HttpPost("forgotPassword")]
        public async Task<ActionResult> ForgotPassword(UserForgotPasswordDto userForgotPasswordDto)
        {
            string email = userForgotPasswordDto.Email;
            User user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                string errorMessage = $"Failed to send reset password email. Unable to find user with the email: '{email}'.";
                _logger.LogError(errorMessage);
                return NotFound(errorMessage);
            }

            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            string callback = Url.Action("Reset", "user", new { resetToken }, Request.Scheme);

            Message message = new Message(new string[] { user.Email }, "Reset password token", callback);
            await _emailSender.SendEmailAsync(message);

            string successMessage = $"Successfully sent the reset password email to the user with the email: '{email}'.";
            _logger.LogInformation(successMessage);
            return Ok(successMessage);
        }

        // POST: user/verifyResetToken
        [HttpPost("verifyResetToken")]
        public async Task<ActionResult> VerifyResetToken(UserVerifyResetTokenDto userVerifyResetTokenDto)
        {
            string email = userVerifyResetTokenDto.Email;
            User user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                string errorMessage = $"Unable to find user with the email '{email}'.";
                _logger.LogError(errorMessage);
                return NotFound(errorMessage);
            }

            string resetToken = userVerifyResetTokenDto.ResetToken;

            bool resetTokenIsValid = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetToken);

            if (!resetTokenIsValid)
            {
                string errorMessage = $"Invalid reset token associated with the user '{email}'.";
                _logger.LogError(errorMessage);
                return BadRequest(errorMessage);
            }

            string successMessage = $"Successfully verified the reset password token for the user with the email: '{email}'.";
            _logger.LogInformation(successMessage);
            return Ok(successMessage);
        }

        // POST: user/resetPassword
        [HttpPost("resetPassword")]
        public async Task<ActionResult> ResetPassword(UserResetPasswordDto userResetPasswordDto)
        {
            string email = userResetPasswordDto.Email;
            User user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                string errorMessage = $"Unable to find user with the email '{email}'.";
                _logger.LogError(errorMessage);
                return NotFound(errorMessage);
            }

            IdentityResult resetPassResult = await _userManager.ResetPasswordAsync(user, userResetPasswordDto.ResetToken, userResetPasswordDto.Password);

            if (!resetPassResult.Succeeded)
            {
                string errorMessage = $"Failed to reset the password associated with the user '{email}'.";
                _logger.LogError(errorMessage);
                return BadRequest(errorMessage);
            }

            string successMessage = $"Successfully reset the password for the user with the email: '{email}'.";
            _logger.LogInformation(successMessage);
            return Ok(successMessage);
        }
    }
}