using CbExamples.NUnitPlaywright.Infra;
using CbExamples.NUnitPlaywright.Pages.SauceDemo;
using CloudBeat.Kit.Common.Attributes;
using CloudBeat.Kit.Common.Enums;
using CloudBeat.Kit.Common.Models;
using CloudBeat.Kit.NUnit;
using CloudBeat.Kit.Playwright;
using Microsoft.Playwright;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CbExamples.NUnitPlaywright.Tests.SauceDemo
{
    [CbTestMode(CbTestModeEnum.Web)]
    public class ExampleTests : PageBase
    {
        [Test(Description = "Example of Assert failure outside of a step")]
        public async Task AssertOutsideOfStep()
        {
            var loginPage = new LoginPage(Page);
            await loginPage.Open();
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
        public async Task WarningsExample()
        {
            var loginPage = new LoginPage(Page);
            await loginPage.Open();
            loginPage.AssertPageOpen();

            CbNUnit.HasWarnings();
        }

        [Test(Description = "Example of CbNUnit.Step")]
        public void StepExample()
        {
            LoginPage loginPage = new LoginPage(Page);

            CbNUnit.Step("step 1", async () => {
                await loginPage.Open();
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

        [Test(Description = "Example of APIRequest")]
        public async Task ApiExample()
        {
            var playwright = await Playwright.CreateAsync();
            var pwRequestContext = await playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions()
            {
                BaseURL = "https://api.github.com",
                ExtraHTTPHeaders = new List<KeyValuePair<string, string>>()
                {
                     new KeyValuePair<string, string>("Context-Type", "application/json"),
                }
            });

            var cbRequestContext = new CbAPIRequestContextWrapper(pwRequestContext, CbNUnit.Current.Reporter);

            var response = await cbRequestContext.GetAsync("/");
        }
    }
}
