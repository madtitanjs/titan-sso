using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SSO.Storage
{
    public interface IBaseStore : IDisposable
    {
        DbContext Context { get; set; }
        Task SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}