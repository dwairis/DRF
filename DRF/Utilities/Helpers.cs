using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

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
        public static string MaskEmailAddress(string input, char maskChar = '*')
        {
            string pattern = @"(?<=[\w]{2})[\w\-._\+%]*(?=[\w]{1}@)";
            return Regex.Replace(input, pattern, m => new string(maskChar, m.Length));

        }
        public static string MaskPhoneNumber(string phoneNumber, char maskChar = '*')
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return phoneNumber;
            }

            // Remove non-numeric characters
            string digitsOnly = new string(phoneNumber.Where(c => char.IsDigit(c)).ToArray());

            // Apply mask to all digits except the last 4
            int maskLength = digitsOnly.Length - 4;
            return maskLength > 0
                ? digitsOnly.Replace(digitsOnly.Substring(0, maskLength), new string(maskChar, maskLength))
                : digitsOnly;
        }
        public static string ToSHA3(string value)
        {

            SHA512CryptoServiceProvider sh = new SHA512CryptoServiceProvider();
            sh.ComputeHash(ASCIIEncoding.ASCII.GetBytes(value));
            byte[] re = sh.Hash;
            StringBuilder sb = new StringBuilder();
            foreach (byte b in re)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
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
