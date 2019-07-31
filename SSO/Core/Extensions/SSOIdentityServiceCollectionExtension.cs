using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SSO.Core.Context;
using SSO.Core.Identity.Models;
using SSO.Core.Validators;
using System;

namespace SSO.Core.Extensions
{
    public static class SSOIdentityServiceCollectionExtension
    {
        /// <summary>
        /// Add SSO Identity context and models
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSSOIdentity(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            services
                .AddIdentity<User, Role>()
                .AddEntityFrameworkStores<SSODbContext>()
                .AddDefaultTokenProviders();

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<SSODbContext>(options);

            return services;
        }

        public static IServiceCollection AddOIDC(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            services
                .AddIdentityServer()
                .AddAspNetIdentity<User>()
                // replace with actual signing credential
                .AddDeveloperSigningCredential()
                .AddConfigurationStore<SSOConfigDbContext>(op =>
                {
                    #region Clients
                    op.Client = new TableConfiguration("OidcClient");
                    op.ClientGrantType = new TableConfiguration("OidcClientGrantType");
                    op.ClientRedirectUri = new TableConfiguration("OidcClientRedirectUri");
                    op.ClientPostLogoutRedirectUri = new TableConfiguration("OidcClientPostLogoutRedirectUri");
                    op.ClientScopes = new TableConfiguration("OidcClientScopes");
                    op.ClientSecret = new TableConfiguration("OidcClientSecret");
                    op.ClientClaim = new TableConfiguration("OidcClientClaims");
                    op.ClientIdPRestriction = new TableConfiguration("OidcClientIdPRestriction");
                    op.ClientCorsOrigin = new TableConfiguration("OidcClientCorsOrigin");
                    op.ClientProperty = new TableConfiguration("OidcClientProperty");
                    #endregion
                    #region Resources
                    op.IdentityResource = new TableConfiguration("OidcIdentityResource");
                    op.IdentityClaim = new TableConfiguration("OidcIdentityClaim");
                    op.IdentityResourceProperty = new TableConfiguration("OidcIdentityResourceProperty");
                    op.ApiResource = new TableConfiguration("OidcApiResource");
                    op.ApiClaim = new TableConfiguration("OidcApiClaims");
                    op.ApiSecret = new TableConfiguration("OidcApiSecret");
                    op.ApiScope = new TableConfiguration("OidcApiScope");
                    op.ApiScopeClaim = new TableConfiguration("OidcApiScopeClaim");
                    op.ApiResourceProperty = new TableConfiguration("OidcApiResourceProperty");
                    #endregion
                    op.ConfigureDbContext = options;
                })
                .AddOperationalStore<SSOPersistDbContext>(op =>
                                {
                                    op.PersistedGrants = new TableConfiguration("OidcPersistedGrants");
                                    op.DeviceFlowCodes = new TableConfiguration("OidcDeviceFlowCodes");
                                    op.ConfigureDbContext = options;
                                })
                .AddSecretValidator<SimpleSecretValidator>();

            return services;
        }
    }
}
