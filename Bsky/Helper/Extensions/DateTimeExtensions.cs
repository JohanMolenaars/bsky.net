
using System;

namespace Bsky.Net.Helper.Extensions
{
    public static class DateTimeExtensions
    {
        public static string toISOstring(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }
    }
}
