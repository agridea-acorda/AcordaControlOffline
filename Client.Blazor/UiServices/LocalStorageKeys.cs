namespace Agridea.Acorda.AcordaControlOffline.Client.Blazor.UiServices
{
    public class LocalStorageKeys
    {
        public const string Mandates = "mandates";
        public const string MandateDetail = "mandateDetail";
        
        public static string MandateDetailKey(int farmId)
        {
            return $"{MandateDetail}_{farmId}";
        }
    }
}
