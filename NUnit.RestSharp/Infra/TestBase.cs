using CloudBeat.Kit.Common.Wrappers;
using CloudBeat.Kit.NUnit;
using CloudBeat.Kit.NUnit.Attributes;
using NUnit.Framework;
using RestSharp;

namespace CbExamples.NUnit.RestSharp.Infra
{
    [CbNUnitTest]
    [TestFixture]
    public class TestBase
	{
		protected RestClient client;

		public TestBase()
		{
            CbHttpMessageHandler handler = null;
            if (CbNUnit.Current?.Reporter != null)
            {
                handler = new CbHttpMessageHandler(CbNUnit.Current.Reporter);
            }
            
            var restClientOptions = new RestClientOptions
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true,
                ConfigureMessageHandler = _ => handler
            };
            client = new RestClient(restClientOptions);
        }
	}
}

