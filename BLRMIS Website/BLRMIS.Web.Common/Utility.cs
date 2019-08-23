using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BLRMIS.Web.Common
{
   public static class Utility
    {
        private static readonly AppSettings AppSettings;
        public static string RandomNumber6Digit()
        {
            Random generator = new Random();
            return generator.Next(0, 999999).ToString("D6");
        }

        public static string LimitString(string text, int limit)
        {
            if (!string.IsNullOrEmpty(text))
            {
                return text.Length > limit ? text.Substring(0, limit) : text;
            }
            return string.Empty;
        }

    
    }
}
