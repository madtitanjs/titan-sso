using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSO.Extensions
{
    public static class HtmlExtensions
    {
        public static string ModelValidationSummary<T>(this IHtmlHelper<T> htmlHelper)
        {
            string ret = string.Empty;
            var modelState = htmlHelper.ViewData.ModelState;
            foreach (var key in modelState.Keys)
            {
                foreach(var err in modelState[key].Errors)
                {
                    ret += $"<p>{err.ErrorMessage}</p>";
                }
            }
            return ret;
        }
    }
}
