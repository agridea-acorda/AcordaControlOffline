using Blazored.Toast.Services;

namespace Agridea.Acorda.AcordaControlOffline.Client.Blazor.UiServices
{
    public class ToastMessages
    {
        public const string SuccessHeader = "Succès";
        public const string ErrorHeader = "Erreur";
        public const string Success = "Action réalisée avec succès.";
    }

    public static class ToastExtensions
    {
        public static void Success(this IToastService toast)
        {
            toast.ShowSuccess(ToastMessages.Success, ToastMessages.SuccessHeader);
        }
    }
}
