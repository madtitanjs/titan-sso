using SSO.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SSO.Core.Helpers
{
    public static class EnumeratorHelper
    {
        public static IEnumerable<SelectResult> ToList<T>() where T : struct
        {
            var values = Enum.GetValues(typeof(T)).Cast<T>().Select(s => new SelectResult(Convert.ToInt32(s).ToString(), s.ToString()));
            return values.AsEnumerable();
        }

    }
}
