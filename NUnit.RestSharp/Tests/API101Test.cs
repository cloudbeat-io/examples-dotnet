using CbExamples.NUnit.RestSharp.Infra;
using CloudBeat.Kit.NUnit;
using NUnit.Framework;
using RestSharp;

namespace CbExamples.NUnit.RestSharp.Tests
{
    public class API101Test : TestBase
	{
        [Test]
        public void AllCustomersTest()
		{
			var request = new RestRequest("https://api-101.glitch.me/customers", Method.Get);
			var response = client.Get(request);
        }

        [Test(Description = "Example of getting environment variables from CB")]
        public void EnvExample()
        {
            // expects currently selected environment to have a variable named TestParam
            if (CbNUnitContext.IsEnabled)
            {
                var param = CbNUnit.GetParameter("TestParam");
                TestContext.Out.WriteLine($"Environment variable TestParam={param ?? "UNDEFINED"}");
            }
        }
    }
}

