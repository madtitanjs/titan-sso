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
        public static IEnumerable<string> ModelValidationSummary<T>(this IHtmlHelper<T> htmlHelper)
        {
            List<string> errors = new List<string>();
            var modelState = htmlHelper.ViewData.ModelState;
            foreach (var key in modelState.Keys)
            {
                foreach(var err in modelState[key].Errors)
                {
                    errors.Add(err.ErrorMessage);
                }
            }
            return errors;
        }
    }
}
