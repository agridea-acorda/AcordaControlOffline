using System;
using System.Collections.Generic;
using System.Linq;

namespace Agridea.Acorda.AcordaControlOffline.Client.Blazor.UiServices
{
    public static class MiscExtensions
    {
        public static string ToDetailedTime(this DateTime dt)
        {
            return $"{dt.ToLongTimeString()} {dt.Millisecond:000}ms";
        }

        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source)
        {
            return source.Select((item, index) => (item, index));
        }
    }
}
