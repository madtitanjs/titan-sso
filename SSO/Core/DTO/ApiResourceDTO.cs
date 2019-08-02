using System;
using System.Collections.Generic;
using System.Text;

namespace SSO.Core.DTO
{
    public class ApiResourceDTO
    {
        public ApiResourceDTO()
        {
            UserClaims = new List<string>();
            Secrets = new List<SecretDTO>();
            Scopes = new List<ScopeDTO>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public bool Enabled { get; set; } = true;

        public List<string> UserClaims { get; set; }

        public string UserClaimsItems { get; set; }

        public List<SecretDTO> Secrets { get; set; }
        public List<ScopeDTO> Scopes { get; set; }
    }
}
