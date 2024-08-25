using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gurung.Wrapper.Helpers
{
    public static class ValidationHelpers
    {
        /// <summary>
        /// Validates that a given string can be parsed to a DateTime object
        /// </summary>
        /// <param name="date">a date string</param>
        /// <param name="format">format to try attempt the DateTime.TryParseExact operation with</param>
        /// <returns>true if date is valid and can be parsed. false if not.</returns>
        public static bool IsDateValid(string date, string format = "yyyy-MM-dd")
        {
            if (string.IsNullOrEmpty(date)) return false;
            return DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out _);
        }
    }
}
