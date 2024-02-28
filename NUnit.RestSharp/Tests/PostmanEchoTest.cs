using CbExamples.NUnit.RestSharp.Infra;
using RestSharp;
using System;
using FluentAssertions;
using NUnit.Framework;

namespace CbExamples.NUnit.RestSharp.Tests
{
	public class PostmanEchoTest : TestBase
	{
		[Test]
		public void RequestMethodsTest()
		{
			var request = new RestRequest("https://postman-echo.com/get", Method.Get);
			var response = client.Execute(request);
			response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
			
			/*Assert.Multiple(() =>
			{
				Assert.That((int)response.StatusCode, Is.EqualTo(201));
				Assert.That(response.StatusDescription, Is.EqualTo("Not found"));
			});*/
		}
	}
}
