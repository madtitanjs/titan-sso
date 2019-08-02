﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SSO.Core.DTO
{
    public class IdentityResourceDTO
    {
        public IdentityResourceDTO()
        {
            UserClaims = new List<string>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public bool Enabled { get; set; } = true;

        public bool ShowInDiscoveryDocument { get; set; } = true;

        public bool Required { get; set; }

        public bool Emphasize { get; set; }

        public List<string> UserClaims { get; set; }

        public string UserClaimsItems { get; set; }
    }
}
