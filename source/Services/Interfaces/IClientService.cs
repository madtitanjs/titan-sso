using System.Threading.Tasks;
using SSO.Core.DTO;

namespace Services
{
    public interface IClientService
    {
        Task<ClientDTO> AddClientAsync(ClientDTO dto);
        Task DeleteClientAsync(int clientId);
        Task<ClientDTO> GetClientAsync(int id);
        Task<PagedQuery<ClientDTO>> SearchClient(SearchDTO searchDTO);
        Task<ClientDTO> UpdateClientAsync(ClientDTO dto);
    }
}