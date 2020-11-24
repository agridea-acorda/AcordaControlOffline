using System.IO;
using System.Text;
using Xunit.Abstractions;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.Tests
{
    class TestOutputWriter : TextWriter
    {
        readonly ITestOutputHelper output_;
        public TestOutputWriter(ITestOutputHelper output)
        {
            output_ = output;
        }
        public override Encoding Encoding => Encoding.UTF8;

        public override void WriteLine(string message)
        {
            output_.WriteLine(message);
        }
        public override void WriteLine(string format, params object[] args)
        {
            output_.WriteLine(format, args);
        }
    }
}