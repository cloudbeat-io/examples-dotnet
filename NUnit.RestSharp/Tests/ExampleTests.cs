using CbExamples.NUnit.RestSharp.Infra;
using CloudBeat.Kit.Common.Models;
using CloudBeat.Kit.NUnit;
using NUnit.Framework;
using System.Linq;

namespace CbExamples.NUnit.RestSharp.Tests
{
    public class ExampleTests : TestBase
    {
        [Test(Description = "Example of adding test output data/attributes and getting input parameters")]
        public void OutputDataAndParamsExample()
        {
            // get input parameters passed from CloudBeat and print to console log
            var testData = CbNUnit.GetTestData();

            if (testData.Any())
            {
                var i = 1;
                foreach (var row in testData)
                {
                    TestContext.Progress.WriteLine($"Parameter row #{i++}: {string.Join(',', row)}");
                }
            }
            else
            {
                TestContext.Progress.WriteLine("No parameters were specified");
            }

            // set output data + attributes
            CbNUnit.AddOutputData("foo", "bar data");
            CbNUnit.AddTestAttribute("foo", "bar attribute");
        }

        [Test(Description = "Example of getting environment variables from CB")]
        public void EnvExample()
        {
            if (CbNUnit.IsRunningFromCB())
            {
                var envName = CbNUnit.GetEnvironmentName();
                TestContext.Progress.WriteLine($"Currently selected environment: {envName ?? "UNDEFINED"}");

                var envVal = CbNUnit.GetEnvironmentValue("TestParam");
                TestContext.Progress.WriteLine($"Environment variable TestParam: {envVal ?? "UNDEFINED"}");
            }
        }

        [Test(Description = "Example of setting failure reason")]
        public void FailureReasonExample()
        {
            try
            {
                Assert.Fail("This test needs to be investigated");
            } 
            catch
            {
                CbNUnit.SetFailureReason(FailureReasonEnum.ToInvestigate);
                throw;
            }
        }

        [Test(Description = "Example of marking test with warnings")]
        public void WarningsExample()
        {
            CbNUnit.Step("foo", () => {});
            CbNUnit.Step("bar", () => {});

            CbNUnit.HasWarnings();
        }
    }
}

