using Xunit;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Tests
{
    public class InspectionTests
    {
        [Fact]
        public void Can_construct_new_inspection()
        {
            var inspection = TestDataHelper.ConstructInspection();
            TestDataHelper.InspectionShouldBeSuchAsConstructed(inspection);
        }
    }
}
