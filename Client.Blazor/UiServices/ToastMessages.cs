using System;
using System.Net.Http;
using Blazored.Toast.Services;
using CSharpFunctionalExtensions;

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

        public static void Error(this IToastService toast, string message)
        {
            toast.ShowError(message, ToastMessages.ErrorHeader);
        }

        public static string ToUserErrorMessage(this Exception e)
        {
            string errorMessage = "Echec de la connexion au serveur";
            errorMessage += e switch
            {
                HttpRequestException _ => ": serveur inatteignable.",
                _ => ": erreur inconnue;"
            };
            return errorMessage;
        }
        public static string ToLogErrorMessage(this Exception e)
        {
            string errorMessage = $"Api call failed at {DateTime.Now.ToDetailedTime()}, no response received. " +
                                  $"Exception details: {e}";
            return errorMessage;
        }

        public static string ToLogErrorMessage<T>(this Result<T> apiCallResult)
        {
            return $"Failed to fetch {typeof(T).Name} from api. Reason: {apiCallResult.Error}";
        }

        public static string ToUserErrorMessage<T>(this Result<T> apiCallResult)
        {
            string message = "Erreur retournée par le serveur";
            int httpStatusCodeIndex = apiCallResult.Error.IndexOf("HttpStatusCode =", StringComparison.Ordinal);
            if (httpStatusCodeIndex <= -1)
                return message + ", erreur inconnue.";

            string httpStatusCode = apiCallResult.Error.Substring(httpStatusCodeIndex);
            return message + $", erreur d'api, {httpStatusCode}";
        }
    }
}
