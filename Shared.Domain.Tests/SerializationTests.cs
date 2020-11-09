using System.IO;
using FluentAssertions;
using FluentAssertions.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Tests
{
    public class SerializationTests
    {
        [Fact]
        public void NewtonSoftJson_Can_Deserialize_Then_Serialize_Mandate()
        {
            var json = File.ReadAllText("./Data/mandate.json");
            var mandate = JsonConvert.DeserializeObject<Mandate.Mandate>(json);
            mandate.Should().NotBeNull();

            var jsonReturned = JsonConvert.SerializeObject(mandate);
            JToken.Parse(jsonReturned).Should().BeEquivalentTo(JToken.Parse(json));
        }
    }
}
