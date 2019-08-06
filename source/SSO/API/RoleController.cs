using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using SSO.Core.DTO;
using SSO.Core.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSO.API
{
    [Route("api/[controller]")]
    public class RoleController : Controller
    {
        private readonly IIdentityService identityService;

        public RoleController(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedQuery<RoleDTO>>> GetRolesAsync(SearchDTO searchDTO)
        {
            var result = await identityService.SearchRole(searchDTO);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDTO>> GetRoleAsync(Guid id)
        {
            var result = await identityService.GetRoleById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<RoleDTO>> CreateRoleAsync(RoleDTO dto)
        {
            var result = await identityService.CreateRole(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RoleDTO>> UpdateRoleAsync(Guid id, [FromBody] RoleDTO dto)
        {
            var result = await identityService.UpdateRole(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveRoleAsync(Guid id)
        {
            await identityService.RemoveRole(id);
            return Ok();
        }
    }
}
