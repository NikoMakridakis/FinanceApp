using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        public UserController(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/user
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<UserDto>>> GetUsers()
        {
            IReadOnlyList<User> user = await _repo.GetUsersAsync();
            return Ok(_mapper.Map<IReadOnlyList<UserDto>>(user));
        }

        // GET: api/user/{userId}
        [HttpGet("{userId}", Name = "GetUser")]
        public async Task<ActionResult<UserDto>> GetUser(int userId)
        {
            if (!_repo.UserByUserIdExists(userId))
            {
                return NotFound($"Unable to find user with ID '{userId}'.");
            }

            User user = await _repo.GetUserByUserIdAsync(userId);
            return Ok(_mapper.Map<UserDto>(user));
        }

        // POST: api/user
        [HttpPost]
        public async Task<ActionResult<UserDto>> PostUser(UserForCreationDto userForCreationDto)
        {
            User user = _mapper.Map<User>(userForCreationDto);
            await _repo.AddUserAsync(user);
            UserDto userDto = _mapper.Map<UserDto>(user);
            return CreatedAtRoute(nameof(GetUser), new { userId = userDto.UserId }, userDto);
        }

        // PUT: api/user/{userId}
        [HttpPut("{userId}")]
        public async Task<ActionResult<UserDto>> PutUser(int userId, UserForUpdateDto userForUpdateDto)
        {
            User user = await _repo.GetUserByUserIdAsync(userId);

            if (user == null)
            {
                return NotFound($"Unable to find user with ID '{userId}'.");
            }

            _mapper.Map(userForUpdateDto, user);
            await _repo.UpdateUserAsync(user);
            UserDto userDto = _mapper.Map<UserDto>(user);
            return CreatedAtRoute(nameof(GetUser), new { userId = userDto.UserId }, userDto);
        }

        // DELETE: api/user/{userId}
        [HttpDelete("{userId}")]
        public async Task<ActionResult<UserDto>> DeleteUser(int userId)
        {
            User user = await _repo.GetUserByUserIdAsync(userId);

            if (user == null)
            {
                return NotFound($"Unable to find user with ID '{userId}'.");
            }

            await _repo.DeleteUserByUserIdAsync(userId);
            UserDto userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }
    }
}