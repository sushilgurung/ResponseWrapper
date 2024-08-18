using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrapperLibrary.Helpers
{
    public static class CommonHelper
    {
        /// <summary>
        ///  A DateTime extension method that return a DateTime with the time set to "00:00:00:000". The first moment of
        ///     the day. Use "DateTime2" column type in sql to keep the precision.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime StartOfDay(this DateTime @date)
        {
            return new DateTime(@date.Year, @date.Month, @date.Day, 0, 0, 0, 0);
        }

        /// <summary>
        ///     A DateTime extension method that return a DateTime with the time set to "23:59:59:999". The last moment of
        ///     the day. Use "DateTime2" column type in sql to keep the precision.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A DateTime of the day with the time set to "23:59:59:999".</returns>
        public static DateTime EndOfDay(this DateTime @date)
        {
            return new DateTime(@date.Year, @date.Month, @date.Day).AddDays(1).Subtract(new TimeSpan(0, 0, 0, 0, 1));
        }
    }
}
