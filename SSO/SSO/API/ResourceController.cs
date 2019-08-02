using Microsoft.AspNetCore.Mvc;
using Services;
using SSO.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSO.API
{
    [Route("api/[controller]")]
    public class ResourceController : Controller
    {
        private readonly IApiResourceService apiResourceService;
        private readonly IIdentityResourceService identityResourceService;

        public ResourceController(IApiResourceService apiResourceService, IIdentityResourceService identityResourceService)
        {
            this.apiResourceService = apiResourceService;
            this.identityResourceService = identityResourceService;
        }

        [HttpGet("protected")]
        public async Task<ActionResult<PagedQuery<ApiResourceDTO>>> SearchApiResourceAsync(SearchDTO searchDTO)
        {
            var result = await apiResourceService.SearchAsync(searchDTO);
            return Ok(result);
        }

        [HttpGet("protected/{id}")]
        public async Task<ActionResult<ApiResourceDTO>> GetApiResourceAsync(int id)
        {
            var result = await apiResourceService.GetAsync(id);
            return Ok(result);
        }

        [HttpPost("protected")]
        public async Task<ActionResult<ApiResourceDTO>> CreateApiResourceAsync([FromBody] ApiResourceDTO dto)
        {
            var result = await apiResourceService.AddAsync(dto);
            return Ok(result);
        }

        [HttpPut("protected")]
        public async Task<ActionResult<ApiResourceDTO>> UpdateApiResourceAsync([FromBody] ApiResourceDTO dto)
        {
            var result = await apiResourceService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("protected")]
        public async Task<ActionResult> RemoveApiResourceAsync(int id)
        {
            await apiResourceService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet("identity")]
        public async Task<ActionResult<PagedQuery<IdentityResourceDTO>>> SearchIdentityResourceAsync(SearchDTO searchDTO)
        {
            var result = await identityResourceService.SearchAsync(searchDTO);
            return Ok(result);
        }

        [HttpGet("identity/{id}")]
        public async Task<ActionResult<IdentityResourceDTO>> GetIdentityResourceAsync(int id)
        {
            var result = await identityResourceService.GetAsync(id);
            return Ok(result);
        }

        [HttpPost("identity")]
        public async Task<ActionResult<IdentityResourceDTO>> CreateIdentityResourceAsync([FromBody] IdentityResourceDTO dto)
        {
            var result = await identityResourceService.AddIdentityResourceAsync(dto);
            return Ok(result);
        }

        [HttpPut("identity")]
        public async Task<ActionResult<IdentityResourceDTO>> UpdateIdentityResourceAsync([FromBody] IdentityResourceDTO dto)
        {
            var result = await identityResourceService.UpdateIdentityResourceAsync(dto);
            return Ok(result);
        }

        [HttpDelete("identity")]
        public async Task<ActionResult> RemoveIdentityResourceAsync(int id)
        {
            await identityResourceService.DeleteAsync(id);
            return Ok();
        }
    }
}