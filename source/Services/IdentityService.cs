using Microsoft.EntityFrameworkCore;
using SSO.Core.DTO;
using System.Threading.Tasks;
using System.Linq;
using SSO.Core.Context;
using Microsoft.AspNetCore.Identity;
using SSO.Core.Identity.Models;
using SSO.Core.Mapper;
using System;
using System.Security.Claims;
using SSO.Core;

namespace Services
{
    public class IdentityService : IIdentityService
    {
        public UserManager<User> UserManager { get; private set; }
        public RoleManager<Role> RoleManager { get; private set; }

        private readonly SSODbContext Context;

        public IdentityService(UserManager<User> userManager, RoleManager<Role> roleManager, SSODbContext context)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            Context = context;
        }

        public async Task<PagedQuery<UserDTO>> SearchUser(SearchDTO search)
        {
            var query = from obj in Users
                        .Include(o => o.UserRoles)
                        .ThenInclude(o => o.Role)
                        .Include(o => o.Claims)
                        select obj;

            if (!string.IsNullOrWhiteSpace(search.Search))
            {
                var _s = search.Search;
                query = from obj in query
                        where obj.Email.Contains(_s) || obj.Claims.Any(claim => claim.ClaimValue.Contains(_s)) || obj.UserRoles.Any(s => s.Role.Name.Contains(_s))
                        select obj;
            }

            int total = query.Count();
            var users = await query.Skip(search.Start).Take(search.Count).ToArrayAsync();

            var result = new PagedQuery<UserDTO>
            {
                Start = search.Start,
                Count = search.Count,
                Total = total,
                Items = users.Select(i => i.ToDTO()),
                Search = search.Search
            };

            return result;

        }

        public async Task<UserDTO> FindUserBySubjectId(string subjectId)
        {
            var query = from obj in Users
                        .Include(o => o.UserRoles)
                        .ThenInclude(o => o.Role)
                        .Include(o => o.Claims)
                        where obj.Id.ToString() == subjectId
                        select obj;

            var user = await query.FirstOrDefaultAsync();
            return user.ToDTO();
        }

        public async Task<UserDTO> FindUserByEmail(string email)
        {
            var query = from obj in Users
                        .Include(o => o.UserRoles)
                        .ThenInclude(o => o.Role)
                        .Include(o => o.Claims)
                        where obj.Email == email
                        select obj;

            var user = await query.FirstOrDefaultAsync();
            return user.ToDTO();
        }

        public async Task<UserDTO> CreateUser(UserCreateDTO dto)
        {
            var entity = dto.ToEntity();
            entity.SecurityStamp = Guid.NewGuid().ToString();

            var tryCreate = await UserManager.CreateAsync(entity, dto.Password);
            if(!tryCreate.Succeeded)
            {
                throw new Exception(string.Join(",", tryCreate.Errors));
            }

            var _claims = dto.Claims.Select(claim => new Claim(claim.Key, claim.Value));

            if (_claims.Count() > 0) await UserManager.AddClaimsAsync(entity, _claims);

            if(dto.Roles.Count() > 0)
            {
                await UserManager.AddToRolesAsync(entity, dto.Roles);
            }

            return entity.ToDTO();
        }

        public async Task<UserDTO> UpdateUser(Guid id,UserDTO dto)
        {
            var user = await UserManager.FindByIdAsync(id.ToString());
            var userClaims = await UserManager.GetClaimsAsync(user);
            var claimsToAdd = dto.Claims.Select(claim => new Claim(claim.Key, claim.Value));

            await UserManager.RemoveClaimsAsync(user, userClaims);
            await UserManager.AddClaimsAsync(user, claimsToAdd);

            var userRoles = await UserManager.GetRolesAsync(user);

            await UserManager.RemoveFromRolesAsync(user, userRoles);
            await UserManager.AddToRolesAsync(user, dto.Roles);
            

            user.PhoneNumber = dto.PhoneNumber;
            await UserManager.UpdateAsync(user);

            return user.ToDTO();
        }

        public async Task RemoveUserAsync(Guid subjectId)
        {
            var user = await UserManager.FindByIdAsync(subjectId.ToString());
            if (user == null) throw new Exception(Constants.Errors.NotFound);
            await UserManager.DeleteAsync(user);
        }


        public async Task<PagedQuery<RoleDTO>> SearchRole(SearchDTO search)
        {
            var query = from obj in Roles select obj;

            if (!string.IsNullOrWhiteSpace(search.Search))
            {
                var _s = search.Search;
                query = from obj in query
                        where obj.Name.Contains(_s)
                        select obj;
            }

            int total = query.Count();
            var roles = await query.Skip(search.Start).Take(search.Count).ToArrayAsync();

            var result = new PagedQuery<RoleDTO>
            {
                Start = search.Start,
                Count = search.Count,
                Total = total,
                Items = roles.Select(i => i.ToDTO()),
                Search = search.Search
            };

            return result;
        }

        public async Task<RoleDTO> GetRoleById(Guid id)
        {
            var query = from obj in Roles where obj.Id == id select obj;
            var role = await query.FirstOrDefaultAsync();
            return role.ToDTO();
        }

        public async Task<RoleDTO> CreateRole(RoleDTO dto)
        {
            var entity = dto.ToEntity();
            await RoleManager.CreateAsync(entity);
            return entity.ToDTO();
        }

        public async Task<RoleDTO> UpdateRole(Guid id, RoleDTO dto)
        {
            var query = from obj in Roles where obj.Id == id select obj;
            var role = await query.FirstOrDefaultAsync();

            if (role == null) throw new Exception(Constants.Errors.NotFound);

            var toUpdate = dto.ToEntity();
            toUpdate.Id = id;

            await RoleManager.UpdateAsync(toUpdate);

            return toUpdate.ToDTO();
        }

        public async Task RemoveRole(Guid id)
        {
            var query = from obj in Roles where obj.Id == id select obj;
            var role = await query.FirstOrDefaultAsync();

            if (role == null) throw new Exception(Constants.Errors.NotFound);
            await RoleManager.DeleteAsync(role);
        }

        private DbSet<User> Users { get { return Context.Set<User>(); } }
        private DbSet<Role> Roles { get { return Context.Set<Role>(); } }
        private DbSet<UserClaim> UserClaims { get { return Context.Set<UserClaim>(); } }
        private DbSet<UserToken> UserTokens { get { return Context.Set<UserToken>(); } }
        private DbSet<UserLogin> UserLogins { get { return Context.Set<UserLogin>(); } }
        private DbSet<RoleClaim> RoleClaims { get { return Context.Set<RoleClaim>(); } }
    }
}
