using System.Threading.Tasks;
using SSO.Core.DTO;

namespace Services
{
    public interface IApiResourceService
    {
        Task<ApiResourceDTO> AddAsync(ApiResourceDTO dto);
        Task DeleteAsync(int id);
        Task<ApiResourceDTO> GetAsync(int id);
        Task<PagedQuery<ApiResourceDTO>> SearchAsync(SearchDTO search);
        Task<ApiResourceDTO> UpdateAsync(ApiResourceDTO dto);
    }
}