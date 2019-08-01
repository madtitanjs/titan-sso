using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSO.Core.DTO;

namespace SSO.API
{
    [Route("api/[controller]")]
    public class ClientController : Controller
    {
   
        [HttpGet]
        public async Task<ActionResult<PagedQuery<ClientDTO>>> SearchClientAsync(string search = "", int start = 0, int count = 10)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDTO>> GetClientAsync(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<ActionResult<ClientDTO>> CreateClientAsync([FromBody] ClientDTO dto)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public async Task<ActionResult<ClientDTO>> UpdateClientAsync([FromBody] ClientDTO dto)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
