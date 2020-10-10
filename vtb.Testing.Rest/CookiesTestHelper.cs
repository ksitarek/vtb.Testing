using System;
using System.Collections.Generic;
using System.Linq;

namespace vtb.Testing.Rest
{
    public static class CookiesTestHelper
    {
        public static Dictionary<string, string> GetAllCookiesFromHeader(string header)
        {
            var cookieDictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            var values = header.TrimEnd(';').Split(';');
            foreach (var parts in values.Select(c => c.Split(new[] { '=' }, 2)))
            {
                var cookieName = parts[0].Trim();
                string cookieValue;

                if (parts.Length == 1)
                    //Cookie attribute
                    cookieValue = string.Empty;
                else
                    cookieValue = parts[1];

                cookieDictionary[cookieName] = cookieValue;
            }

            return cookieDictionary;
        }
    }
}