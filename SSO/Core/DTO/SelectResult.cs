using System;
using System.Collections.Generic;
using System.Text;

namespace SSO.Core.DTO
{
    public class SelectResult
    {
        public SelectResult(string id, string description)
        {
            Id = id;
            Description = description;
        }
        public string Id { get; set; }
        public string Description { get; set; }
    }
}
