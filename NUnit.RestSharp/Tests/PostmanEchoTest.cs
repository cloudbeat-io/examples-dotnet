using CbExamples.NUnit.RestSharp.Infra;
using RestSharp;
using System;
using FluentAssertions;
using NUnit.Framework;
using CloudBeat.Kit.NUnit;

namespace CbExamples.NUnit.RestSharp.Tests
{
	public class PostmanEchoTest : TestBase
	{
		[Test]
		public void RequestMethodsTest()
		{
            // GET Request
            CbNUnit.Step("GET Request", () =>
            {
                var request = new RestRequest("https://postman-echo.com/get", Method.Get);
                var response = client.Execute(request);
                Assert.Multiple(() =>
                {
                    response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
                    response.Content.Should().Contain("args");
                });
            });
            // POST Raw Text
            CbNUnit.Step("POST Raw Text", () =>
            {
                var request = new RestRequest("https://postman-echo.com/post", Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(new { test = "value" });
                var response = client.Execute(request);
                Assert.Multiple(() =>
                {
                    response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
                    response.Content.Should().Contain("data", "This is expected to be sent back as part of response body.");
                });
            });
            // POST Form Data
            CbNUnit.Step("POST Form Data", () =>
            {
                var request = new RestRequest("https://postman-echo.com/post", Method.Post);
                request.AddParameter("foo1", "bar1");
                request.AddParameter("foo2", "bar2");
                var response = client.Execute(request);
                Assert.Multiple(() =>
                {
                    response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
                    response.Content.Should().Contain("form");
                });
            });
        }
	}
}
