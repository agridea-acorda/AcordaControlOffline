using System.Collections.Generic;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Client.Blazor.Config
{
    public class AppConfiguration : ValueObject, IAppConfiguration
    {
        public string ApiEndpoint { get; set; }
        public string BaseUrl { get; set; }
        public bool IsDev { get; set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ApiEndpoint;
            yield return BaseUrl;
            yield return IsDev;
        }

        public static AppConfiguration Empty => new AppConfiguration();
    }

    public interface IAppConfiguration
    {
        public string ApiEndpoint { get; }
        public string BaseUrl { get; }
        public bool IsDev { get; }
    }
}
