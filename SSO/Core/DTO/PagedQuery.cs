using System;
using System.Collections.Generic;
using System.Text;

namespace SSO.Core.DTO
{
    public class PagedQuery<T>
    {
        public int Start { get; set; }
        public int Count { get; set; }
        public int Total { get; set; }
        public IEnumerable<T> Items { get; set; }
        public string Filter { get; set; }
    }
}
