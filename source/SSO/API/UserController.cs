using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using SSO.Core.DTO;
using SSO.Core.Identity.Models;
using System;
using System.Threading.Tasks;

namespace SSO.API
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IIdentityService identityService;
        private readonly SignInManager<User> signInManager;

        public UserController(IIdentityService identityService, SignInManager<User> signInManager)
        {
            this.identityService = identityService;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public async Task<ActionResult<PagedQuery<UserDTO>>> GetUsersAsync(SearchDTO searchDTO)
        {
            var result = await identityService.SearchUser(searchDTO);
            return Ok(result);
        }

        [HttpGet("{subject}")]
        public async Task<ActionResult<UserDTO>> GetUserAsync(string subject)
        {
            var query = await identityService.FindUserBySubjectId(subject);
            return Ok(query);
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUserAsync(UserCreateDTO dto)
        {
            var user = await identityService.CreateUser(dto);
            return Ok(user);
        }

        [HttpPut("{subject}")]
        public async Task<ActionResult<UserDTO>> UpdateUserAsync(Guid subject, [FromBody] UserDTO dto)
        {
            var user = await identityService.UpdateUser(subject, dto);
            return Ok(user);
        }

        [HttpDelete("{subject}")]
        public async Task<ActionResult> RemoveUserAsync(Guid subject)
        {
            await identityService.RemoveUserAsync(subject);
            return Ok();
        }
    }
}
