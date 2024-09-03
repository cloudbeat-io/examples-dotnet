using CbExamples.MSTest.Infra;
using CbExamples.MSTest.Pages.SauceDemo;
using CloudBeat.Kit.MSTest;

namespace CbExamples.MSTest.Tests.SauceDemo
{
    [TestClass]
    [TestCategory("Login")]
    public class LoginTests : WebDriverTest
    {
        [TestMethod("Standard user login behaviour")]
        [TestCategory("JIRA=ISO-124")]
        [TestCategory("User=Standard")]
        [TestCategory("Nightly")]
        [Priority(1)]
		public void StandardUserTest()
		{
            var loginPage = new LoginPage(Driver);
            loginPage.Open();
            /*loginPage.AssertPageOpen();
            loginPage.EnterUsername("standard_user");
            loginPage.EnterPassword("secret_sauce");
            loginPage.PressLoginButton();*/
        }

        [TestMethod("Locked out user login behaviour")]
        [TestCategory("Regression")]
        [TestCategory("Nightly")]
        [Priority(2)]
        public void LockedOutUserTest()
        {
            var loginPage = new LoginPage(Driver);
            loginPage.Open();
            loginPage.AssertPageOpen();
            loginPage.EnterUsername("standard_user");
            loginPage.EnterPassword("secret_sauce");
            loginPage.PressLoginButton();
            loginPage.AssertLoginErrorMessage("Epic sadface: Sorry, this user has been locked out.");
        }

        [TestMethod("Invalid user login behaviour")]
        [Priority(3)]
        public void InvalidUserTest()
        {
            var loginPage = new LoginPage(Driver);
            loginPage.Open();
            loginPage.AssertPageOpen();
            loginPage.EnterUsername("invalid_user");
            loginPage.EnterPassword("invalid_password");
            loginPage.PressLoginButton();
            loginPage.AssertLoginErrorMessage("Epic sadface: Username and password do not match any user in this service");
        }
        [TestMethod]
        [Ignore("This method must be ignored")]
        [TestCategory("Ignore")]
        public void SkipMe()
        {

        }

        [TestMethod("Example of getting environment variables from CB")]
        public void EnvExample()
        {
            if (CbMSTestContext.IsEnabled)
            {
                var ctx = CbMSTest.Current.MSTestContext;

                const string PARAM_NAME = "TestParam";

                if (ctx != null && ctx.Properties != null && ctx.Properties.Contains(PARAM_NAME))
                {
                    var param = ctx.Properties[PARAM_NAME] as string;
                    Console.Out.WriteLine($"Environment variable TestParam={param}");
                } 
                else
                {
                    throw new Exception($"Environment variable TestParam is not defined");
                }
            }
        }
    }
}

