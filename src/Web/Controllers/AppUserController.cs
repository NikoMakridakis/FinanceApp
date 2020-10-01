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
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserRepository _repo;
        private readonly IMapper _mapper;
        public AppUserController(IAppUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/appUser
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AppUserDto>>> GetAppUsers()
        {
            IReadOnlyList<AppUser> appUser = await _repo.GetAppUsersAsync();
            return Ok(_mapper.Map<IReadOnlyList<AppUserDto>>(appUser));
        }

        // GET: api/appUser/{appUserId}
        [HttpGet("{appUserId}", Name = "GetAppUser")]
        public async Task<ActionResult<AppUserDto>> GetAppUser(int appUserId)
        {
            if (!_repo.AppUserByIdExists(appUserId))
            {
                return NotFound($"Unable to find app user with ID '{appUserId}'.");
            }

            AppUser appUser = await _repo.GetAppUserByIdAsync(appUserId);
            return Ok(_mapper.Map<AppUserDto>(appUser));
        }

        // POST: api/appUser
        [HttpPost]
        public async Task<ActionResult<AppUserDto>> PostAppUser(AppUserForCreationDto appUserForCreationDto)
        {
            AppUser appUser = _mapper.Map<AppUser>(appUserForCreationDto);
            await _repo.AddAppUserAsync(appUser);
            AppUserDto appUserDto = _mapper.Map<AppUserDto>(appUser);
            return CreatedAtRoute(nameof(GetAppUser), new { appUserId = appUserDto.AppUserId }, appUserDto);
        }

        // PUT: api/appUser/{appUserId}
        [HttpPut("{appUserId}")]
        public async Task<ActionResult<AppUserDto>> PutAppUser(int appUserId, AppUserForUpdateDto appUserForUpdateDto)
        {
            AppUser appUser = await _repo.GetAppUserByIdAsync(appUserId);

            if (appUser == null)
            {
                return NotFound($"Unable to find app user with ID '{appUserId}'.");
            }

            _mapper.Map(appUserForUpdateDto, appUser);
            await _repo.UpdateAppUserAsync(appUser);
            AppUserDto appUserDto = _mapper.Map<AppUserDto>(appUser);
            return CreatedAtRoute(nameof(GetAppUser), new { appUserId = appUserDto.AppUserId }, appUserDto);
        }

        // DELETE: api/appUser/{appUserId}
        [HttpDelete("{appUserId}")]
        public async Task<ActionResult<AppUserDto>> DeleteAppUser(int appUserId)
        {
            AppUser appUser = await _repo.GetAppUserByIdAsync(appUserId);

            if (appUser == null)
            {
                return NotFound($"Unable to find app user with ID '{appUserId}'.");
            }

            await _repo.DeleteAppUserByIdAsync(appUserId);
            AppUserDto appUserDto = _mapper.Map<AppUserDto>(appUser);
            return Ok(appUserDto);
        }
    }
}