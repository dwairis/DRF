using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text;

namespace DRF.Utilities
{
    public class Helper
    {
        public static DateTime Today
        {
            get
            {
                var info = TimeZoneInfo.FindSystemTimeZoneById("Arabic Standard Time");
                DateTimeOffset localServerTime = DateTimeOffset.Now;
                DateTimeOffset localTime = TimeZoneInfo.ConvertTime(localServerTime, info);
                return localTime.DateTime;
            }
        }
        public static string RenderModelStateErrors(List<ModelStateEntry> entries)
        {
            if (entries.Count > 0)
            {
                StringBuilder sbHtml = new StringBuilder("<ul>");
                foreach (var modelState in entries)
                {

                    foreach (ModelError error in modelState.Errors)
                    {
                        if (error.ErrorMessage.StartsWith("*"))
                        {
                            sbHtml.Append($"<li>{error.ErrorMessage.Substring(1)}</li>");
                        }
                    }
                }
                sbHtml.Append("</ul>");
                return sbHtml.ToString();
            }
            return string.Empty;
        }

        public static List<string> RenderModelError(ModelStateDictionary modelState)
        {
            List<string> errors = new List<string>();
            if (!modelState.IsValid)
            {
                foreach (var item in modelState.Values)
                {
                    foreach (var error in item.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
            }
            return errors;
        }
    }
}
