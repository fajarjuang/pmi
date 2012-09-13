using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace PMI.Application.Helper
{
    public class CultureHelper
    {
        private static readonly IList<string> cultures = new List<string> { "id", "en" };

        public static string GetDefaultNeutralCulture()
        {
            return cultures[0];
        }

        public static string GetImplementedCulture(string culture)
        {
            if (string.IsNullOrEmpty(culture))
                return GetDefaultNeutralCulture();

            // kalau culture ada di dalam cultures, maka pakai langsung. Kalau tidak ada, Indonesia.
            if (cultures.Where(c => c.Equals(culture, StringComparison.InvariantCultureIgnoreCase)).Count() > 0)
                return GetNeutralCulture(culture);
            else
                return GetDefaultNeutralCulture();
        }

        public static string GetCurrentCulture()
        {
            return Thread.CurrentThread.CurrentCulture.Name;
        }

        public static string GetCurrentNeutralCulture()
        {
            return GetNeutralCulture(GetCurrentCulture());
        }

        public static string GetNeutralCulture(string culture)
        {
            if (culture.Length <= 2)
                return culture;

            return culture.Substring(0, 2);
        }
    }
}