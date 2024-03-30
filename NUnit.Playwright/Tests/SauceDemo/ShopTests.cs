using System;
using CloudBeat.Kit.Common.Attributes;
using CloudBeat.Kit.Common.Enums;
using NUnit.Framework;
using CbExamples.NUnitPlaywright.Pages.SauceDemo;
using CbExamples.NUnitPlaywright.Infra;
using System.Threading.Tasks;

namespace CbExamples.NUnitPlaywright.Tests.SauceDemo
{
    [CbTestMode(CbTestModeEnum.Web)]
    public class ShopTests : PageBase
    {
        [Description("Test add/remove buttons")]
        [TestCase("standard_user", "secret_sauce")]
        [TestCase("problem_user", "secret_sauce")]
        [Category("ShoppingCart")]
        [Category("WithLogin")]
        public async Task ProductsAddToRemoveFromCart(string username, string password)
		{
            var loginPage = new LoginPage(Page);
            await loginPage.Open();
            loginPage.AssertPageOpen();
            await loginPage.EnterUsername(username);
            await loginPage.EnterPassword(password);
            await loginPage.PressLoginButton();
            loginPage.AssertLoginSuccess();
            var productsPage = new ProductsPage(Page);
            await productsPage.AssertProductsCount(6);
            await productsPage.ClickAddToCartButton(0);
            await productsPage.AssertPriceBarButtonText(0, "Remove");
            await productsPage.ClickRemoveButton(0);
            await productsPage.AssertPriceBarButtonText(0, "Add to cart");
        }
	}
}

