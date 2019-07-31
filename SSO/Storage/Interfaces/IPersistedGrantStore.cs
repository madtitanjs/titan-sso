using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;

namespace SSO.Storage
{
    public interface IPersistedGrantStore
    {
        void Dispose();
        Task<bool> ExistsPersistedGrantsAsync(string subjectId);
        Task<IOrderedQueryable<PersistedGrant>> GetPersistedGrantsByUser(string subjectId, int start, int count, CancellationToken cancellationToken = default(CancellationToken));
        Task<PersistedGrant> GetPersitedGrantAsync(string key, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> RemovePersistedGrantAsync(string key, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> RemovePersistedGrantsAsync(string subjectId, CancellationToken cancellationToken = default(CancellationToken));
    }
}