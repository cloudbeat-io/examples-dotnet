using CbExamples.NUnit.RestSharp.Infra;
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
    }
}

