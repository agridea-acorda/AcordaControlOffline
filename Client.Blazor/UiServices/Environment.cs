namespace Agridea.Acorda.AcordaControlOffline.Client.Blazor.UiServices
{
    public class Environment
    {
#if DEBUG
        public const string BaseUrl = "";
        public const bool IsDebug = true;
#else
        public const string BaseUrl = "/AcordaControlOffline";
        public const bool IsDebug = false;
#endif

    }
}
