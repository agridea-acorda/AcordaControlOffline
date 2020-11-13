namespace Agridea.Acorda.AcordaControlOffline.Client.Blazor.UiServices
{
    public class Config
    {
#if DEBUG
        public const string BaseUrl = "";
#else
        public const string BaseUrl = "/AcordaControlOffline";
#endif
    }
}
