using System;

namespace Agridea.Acorda.AcordaControlOffline.Client.Blazor.UiServices
{
    public static class MiscExtensions
    {
        public static string ToDetailedTime(this DateTime dt)
        {
            return $"{dt.ToLongTimeString()} {dt.Millisecond:000}ms";
        }
    }
}
