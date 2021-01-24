using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateList;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Tests;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;
using Mandate = Agridea.Acorda.AcordaControlOffline.Shared.Domain.Mandate.Mandate;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.Tests
{
    public class HashTests
    {
        [Fact]
        void Can_compute_md5_hash()
        {
            var inspection = TestDataHelper.ConstructInspection();
            var checklist = TestDataHelper.ConstructChecklist();
            var mandate = new Mandate(1, new []{inspection});
            var mergePackage = MergePackage.FromDomain(mandate, new[] {checklist});
            var md5 = JsonConvert.SerializeObject(mergePackage).ComputeMd5Hash();
            md5.Should().NotBeEmpty();
            md5.Length.Should().Be(32);
        }

        [Fact]
        void Can_compute_sha256_hash()
        {
            var inspection = TestDataHelper.ConstructInspection();
            var checklist = TestDataHelper.ConstructChecklist();
            var mandate = new Mandate(1, new[] { inspection });
            var mergePackage = MergePackage.FromDomain(mandate, new[] { checklist });
            var hash = JsonConvert.SerializeObject(mergePackage).ComputeSha256Hash();
            hash.Should().NotBeEmpty();
            hash.Length.Should().Be(64);
        }
    }
}
