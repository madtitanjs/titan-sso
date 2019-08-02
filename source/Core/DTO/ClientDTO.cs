using SSO.Core.IdentityServer;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSO.Core.DTO
{
    public class ClientDTO
    {
        public int Id { get; set; } = 0;
        public string ClientId { get; set; }
        public bool AllowAccessTokensViaBrowser { get; set; }
        public ClientType ClientType { get; set; }
        public List<string> RedirectUris { get; set; }
        public List<string> PostLogoutRedirectUris { get; set; }
        public string ProtocolType { get; set; } = Constants.Client.ProtocolType.OIDC;
        public List<string> AllowedGrantTypes { get; set; }
        public List<string> AllowedCorsOrigins { get; set; }
        public List<string> AllowedScopes { get; set; }
        public List<string> Claims { get; set; }
        public List<SecretDTO> Secrets { get; set; }

    }
}
