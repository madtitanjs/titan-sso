using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SSO.Core.DTO;
using SSO.Core.Identity.Models;

namespace Services
{
    public interface IIdentityService
    {
        RoleManager<Role> RoleManager { get; }
        UserManager<User> UserManager { get; }

        Task<RoleDTO> CreateRole(RoleDTO dto);
        Task<UserDTO> CreateUser(UserCreateDTO dto);
        Task<UserDTO> FindUserByEmail(string email);
        Task<UserDTO> FindUserBySubjectId(string subjectId);
        Task<RoleDTO> GetRoleById(Guid id);
        Task RemoveRole(Guid id);
        Task RemoveUserAsync(Guid subjectId);
        Task<PagedQuery<RoleDTO>> SearchRole(SearchDTO search);
        Task<PagedQuery<UserDTO>> SearchUser(SearchDTO search);
        Task<RoleDTO> UpdateRole(Guid id, RoleDTO dto);
        Task<UserDTO> UpdateUser(Guid id, UserDTO dto);
    }
}