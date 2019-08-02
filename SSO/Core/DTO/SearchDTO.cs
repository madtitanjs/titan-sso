using System;
using System.Collections.Generic;
using System.Text;

namespace SSO.Core.DTO
{
    public class SearchDTO
    {
        public string Search { get; set; }
        public int Start { get; set; } = 0;
        public int Count { get; set; } = 10;
    }
}
