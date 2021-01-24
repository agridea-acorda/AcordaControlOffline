using System;
using System.Net.Http;
using FluentAssertions;
using Xunit;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.Tests
{
    public class SettingsTests
    {
        [Fact]
        void Can_compare_equal_uris()
        {
            var httpClient = new HttpClient {BaseAddress = new Uri("https://testacordacontrolwebapi.acorda.ch/api/")};
            Uri uriToCompare = new Uri("https://testacordacontrolwebapi.acorda.ch/api/");
            Uri.Compare(httpClient.BaseAddress, uriToCompare, UriComponents.HostAndPort | UriComponents.Path, UriFormat.UriEscaped, StringComparison.Ordinal).Should().Be(0);
        }

        [Fact]
        void Can_compare_different_uris()
        {
            var httpClient = new HttpClient { BaseAddress = new Uri("https://testacordacontrolwebapi.acorda.ch/api/") };
            Uri uriToCompare = new Uri("http://localhost:9421/api/");
            Uri.Compare(httpClient.BaseAddress, uriToCompare, UriComponents.HostAndPort | UriComponents.Path, UriFormat.UriEscaped, StringComparison.Ordinal).Should().NotBe(0);
        }
    }
}
