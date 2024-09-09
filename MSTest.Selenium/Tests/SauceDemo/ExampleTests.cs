using CbExamples.MSTest.Infra;
using CbExamples.MSTest.Pages.SauceDemo;
using CloudBeat.Kit.Common.Models;
using CloudBeat.Kit.MSTest;

namespace CbExamples.MSTest.Tests.SauceDemo
{
    [TestClass]
    public class ExampleTests : WebDriverTest
    {
        [TestMethod("Example of Assert failure outside of a step")]
        public void AssertOutsideOfStep()
        {
            var loginPage = new LoginPage(Driver);
            loginPage.Open();
            Assert.Fail("Assert which was executed outside of a step");
        }

        [TestMethod("Example of adding test output data/attributes and getting input parameters")]
        public void OutputDataAndParamsExample()
        {
            // get input parameters passed from CloudBeat and print to console log
            var testData = CbMSTest.GetTestData();

            if (testData.Any())
            {
                var i = 1;
                foreach (var row in testData)
                {
                    Console.Out.WriteLine($"Parameter row #{i++}: {string.Join(',', row)}");
                }
            }
            else
            {
                Console.Out.WriteLine("No parameters were specified");
            }

            // set output data + attributes
            CbMSTest.AddOutputData("foo", "bar data");
            CbMSTest.AddTestAttribute("foo", "bar attribute");
        }

        [TestMethod("Example of getting environment variables from CB")]
        public void EnvExample()
        {
            if (CbMSTest.IsRunningFromCB())
            {
                var envName = CbMSTest.GetEnvironmentName();
                Console.Out.WriteLine($"Currently selected environment: {envName ?? "UNDEFINED"}");

                var envVal = CbMSTest.GetEnvironmentValue("TestParam");
                Console.Out.WriteLine($"Environment variable TestParam: {envVal ?? "UNDEFINED"}");
            }
        }

        [TestMethod("Example of setting failure reason")]
        public void FailureReasonExample()
        {
            try
            {
                Assert.Fail("This test needs to be investigated");
            } 
            catch
            {
                CbMSTest.SetFailureReason(FailureReasonEnum.ToInvestigate);
                throw;
            }
        }

        [TestMethod("Example of marking test with warnings")]
        public void WarningsExample()
        {
            var loginPage = new LoginPage(Driver);
            loginPage.Open();
            loginPage.AssertPageOpen();

            CbMSTest.HasWarnings();
        }

        [TestMethod("Example of CbMSTest.Step")]
        public void StepExample()
        {
            LoginPage loginPage = new LoginPage(Driver);

            CbMSTest.Step("step 1", () => {
                loginPage.Open();
            });

            CbMSTest.Step("step 2", () => {
                loginPage.AssertPageOpen();
            });

            CbMSTest.Step("step 3", () => {
                CbMSTest.Step("step nested inside step 3", () => {
                    loginPage.AssertPageOpen();
                });
            });
        }
    }
}

