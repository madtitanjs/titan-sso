using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSO.Storage.Extensions
{
    public static class SSOStoreServiceCollectionExtensions
    {
        public static IServiceCollection AddSSOStores(this IServiceCollection services)
        {
            services.AddTransient<IClientStore, ClientStore>();
            services.AddTransient<IApiResourceStore, ApiResourceStore>();
            services.AddTransient<IIdentityStore, IdentityStore>();
            services.AddTransient<IIdentityResourceStore, IdentityResourceStore>();
            services.AddTransient<IPersistedGrantStore, PersistedGrantStore>();
            return services;
        }
    }
}
