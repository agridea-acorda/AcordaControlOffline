using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Agridea.Acorda.AcordaControlOffline.Client.Blazor
{
    public class JsInterop
    {
        public const string InitAdminLte = "initAdminlte";
        public const string ReadCookie = "blazorExtensions.ReadCookie";
        public const string SetCookie = "blazorExtensions.SetCookie";
        public const string RemoveCookie = "blazorExtensions.RemoveCookie";
        public const string IsOnline = "blazorExtensions.IsOnline";
    }

    public class JsInteropActionProxy
    {
        private readonly Action action;
        
        public JsInteropActionProxy(Action action)
        {
            this.action = action;
        }

        [JSInvokable("Client.Blazor")]
        public void InvokeAction()
        {
            action.Invoke();
        }
    }

    public class JsInteropAsyncActionProxy
    {
        private readonly Func<Task> asyncAction;

        public JsInteropAsyncActionProxy(Func<Task> asyncAction)
        {
            this.asyncAction = asyncAction;
        }

        [JSInvokable("Client.Blazor")]
        public void InvokeAction()
        {
            asyncAction.Invoke();
        }
    }

}
