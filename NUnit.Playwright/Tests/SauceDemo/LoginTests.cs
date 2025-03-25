using CbExamples.NUnitPlaywright.Infra;
using CbExamples.NUnitPlaywright.Pages.SauceDemo;
using CloudBeat.Kit.Common.Attributes;
using CloudBeat.Kit.Common.Enums;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CbExamples.NUnitPlaywright.Tests.SauceDemo
{
    [Category("Login")]
    [CbTestMode(CbTestModeEnum.Web)]
    public class LoginTests : PageBase
    {
        [Test(Description = "Standard user login behaviour"), Order(1)]
        [Category("JIRA=ISO-124")]
        [Category("User=Standard")]
        [Category("Nightly")]
        public async Task StandardUserTest()
		{
            var loginPage = new LoginPage(Page);
            await loginPage.Open();
            loginPage.AssertPageOpen();
            await loginPage.EnterUsername("standard_user");
            await loginPage.EnterPassword("secret_sauce");
            await loginPage.PressLoginButton();
            loginPage.AssertLoginSuccess();
        }

        [Test(Description = "Locked out user login behaviour")]
        [Category("Regression")]
        [Category("Nightly")]
        [Order(2)]
        public async Task LockedOutUserTest()
        {
            var loginPage = new LoginPage(Page);
            await loginPage.Open();
            loginPage.AssertPageOpen();
            await loginPage.EnterUsername("locked_out_user");
            await loginPage.EnterPassword("secret_sauce");
            await loginPage.PressLoginButton();
            loginPage.AssertLoginErrorMessage("Epic sadface: Sorry, this user has been locked out.");
        }

        [Test(Description = "Invalid user login behaviour")]
        [Order(3)]
        public async Task InvalidUserTest()
        {
            var loginPage = new LoginPage(Page);
            await loginPage.Open();
            loginPage.AssertPageOpen();
            await loginPage.EnterUsername("invalid_user");
            await loginPage.EnterPassword("invalid_password");
            await loginPage.PressLoginButton();
            loginPage.AssertLoginErrorMessage("Epic sadface: Username and password do not match any user in this service");
        }

        [Test]
        [Ignore("Must be ignored")]
        [Category("Ignore")]
        public void IgnoreMe()
        {
        }
    }
}
