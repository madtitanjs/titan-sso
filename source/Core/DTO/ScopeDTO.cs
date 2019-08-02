using System;
using System.Collections.Generic;
using System.Text;

namespace SSO.Core.DTO
{
    public class ScopeDTO
    {
        public ScopeDTO()
        {
            UserClaims = new List<string>();
        }

        public bool ShowInDiscoveryDocument { get; set; } = true;

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public bool Required { get; set; }

        public bool Emphasize { get; set; }

        public List<string> UserClaims { get; set; }
    }
}
