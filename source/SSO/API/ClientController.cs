using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services;
using SSO.Core.DTO;

namespace SSO.API
{
    [Route("api/[controller]")]
    public class ClientController : Controller
    {
        private readonly IClientService clientService;

        public ClientController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedQuery<ClientDTO>>> SearchClientAsync(SearchDTO searchDTO)
        {
            var result = await clientService.SearchClient(searchDTO);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDTO>> GetClientAsync(int id)
        {
            var result = await clientService.GetClientAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ClientDTO>> CreateClientAsync([FromBody] ClientDTO dto)
        {
            var result = await clientService.AddClientAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ClientDTO>> UpdateClientAsync([FromBody] ClientDTO dto)
        {
            var result = await clientService.UpdateClientAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await clientService.DeleteClientAsync(id);
            return Ok();
        }
    }
}
