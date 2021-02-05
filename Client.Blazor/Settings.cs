namespace Agridea.Acorda.AcordaControlOffline.Client.Blazor
{
    public class Settings
    {
        public const string LocalhostApiBaseAddres = "http://localhost:9421/api/";
        public const string TestApiBaseAddres = "https://testacordacontrolwebapi.acorda.ch/api/";
        public const string ProdApiBaseAddres = "https://acordacontrolwebapi.acorda.ch/api/";
        public const int ThirtyDays = 30;
        public const string CookieName = "settings";

        public string ApiBaseAddres { get; set; } = LocalhostApiBaseAddres;
        public int AuthCookieExpiryDays { get; set; } = ThirtyDays;

        public static Settings Default => new Settings
        {
            ApiBaseAddres = LocalhostApiBaseAddres,
            AuthCookieExpiryDays = ThirtyDays
        };
    }
}
