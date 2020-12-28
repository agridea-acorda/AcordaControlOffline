using System;
using Microsoft.AspNetCore.Components;

namespace Agridea.Acorda.AcordaControlOffline.Client.Blazor
{
    public class PageBase : ComponentBase
    {
        protected override void OnInitialized()
        {
            Console.WriteLine("Hello from PageBase");
            base.OnInitialized();
        }
    }
}
