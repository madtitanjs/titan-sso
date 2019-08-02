using System;
using System.Collections.Generic;
using System.Text;

namespace SSO.Core.DTO
{
    public class SecretDTO
    {
        public string Type { get; set; } = "SharedSecret";
        public string Description { get; set; }
        public string Value { get; set; }
        public DateTime? Expiration { get; set; }
    }
}
