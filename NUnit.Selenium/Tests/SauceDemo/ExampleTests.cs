using CbExamples.NUnit.Infra;
using CbExamples.NUnit.Pages.SauceDemo;
using CloudBeat.Kit.Common.Attributes;
using CloudBeat.Kit.Common.Enums;
using CloudBeat.Kit.Common.Models;
using CloudBeat.Kit.NUnit;
using NUnit.Framework;
using System.Linq;

namespace CbExamples.NUnit.Tests.SauceDemo
{
    [CbTestMode(CbTestModeEnum.Web)]
    public class ExampleTests : WebDriverTest
    {
        [Test(Description = "Example of Assert failure outside of a step")]
        public void AssertOutsideOfStep()
        {
            var loginPage = new LoginPage(Driver);
            loginPage.Open();
            Assert.Fail("Assert which was executed outside of a step");
        }

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
                    Log.Info($"Parameter row #{i++}: {string.Join(',', row)}");
                }
            }
            else
            {
                Log.Info("No parameters were specified");
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
                Log.Info($"Currently selected environment: {envName ?? "UNDEFINED"}");

                var envVal = CbNUnit.GetEnvironmentValue("TestParam");
                Log.Info($"Environment variable TestParam: {envVal ?? "UNDEFINED"}");
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
            var loginPage = new LoginPage(Driver);
            loginPage.Open();
            loginPage.AssertPageOpen();

            CbNUnit.HasWarnings();
        }

        [Test(Description = "Example of CbMSTest.Step")]
        public void StepExample()
        {
            LoginPage loginPage = new LoginPage(Driver);

            CbNUnit.Step("step 1", () => {
                loginPage.Open();
            });

            CbNUnit.Step("step 2", () => {
                loginPage.AssertPageOpen();
            });

            CbNUnit.Step("step 3", () => {
                CbNUnit.Step("step nested inside step 3", () => {
                    loginPage.AssertPageOpen();
                });
            });
        }
    }
}

