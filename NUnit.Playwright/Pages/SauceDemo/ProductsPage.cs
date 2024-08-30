using System.Threading.Tasks;
using CbExamples.NUnitPlaywright.Infra;
using Microsoft.Playwright;
using NUnit.Framework;

namespace CbExamples.NUnitPlaywright.Pages.SauceDemo
{
    public class ProductsPage : PageObjectBase
    {
        public ProductsPage(IPage page) : base(page)
        {
        }

        #region Page Elements

        private ILocator AddToCartButtonList => page.Locator("//button[text()='Add to cart']");

        private ILocator RemoveButtonList => page.Locator("//button[text()='Remove']");

        private ILocator PriceBarList => page.Locator("pricebar");

        #endregion

        public async Task AssertProductsCount(int expectedProductsCount)
        {
            if (AddToCartButtonList == null)
                Assert.Fail("No \"Add to cart\" buttons are found");
            Assert.That(
                await AddToCartButtonList.CountAsync(), Is.EqualTo(expectedProductsCount),
                "Assert amount of products");
        }

        public async Task AssertPriceBarButtonText(int priceBarIndex, string expectedText)
        {
            var priceBarList = PriceBarList;
            if (priceBarList == null)
                Assert.Fail("No price bar elements are found");
            if (await priceBarList.CountAsync() - 1 < priceBarIndex)
                Assert.Fail("There are less price bar elements than expected");
            var selectedPriceBar = priceBarList.Nth(priceBarIndex);
            var button = selectedPriceBar.Locator("button");
            if (button == null)
                Assert.Fail("No button inside price bar element is found");
            Assert.That(
                expectedText, Is.EqualTo(await button.InnerTextAsync()));
        }

        public async Task ClickAddToCartButton(int buttonIndex)
        {
            var addToCartButtonList = AddToCartButtonList;
            if (addToCartButtonList == null)
                Assert.Fail("No \"Add to cart\" buttons are found");
            if (await addToCartButtonList.CountAsync() - 1 < buttonIndex)
                Assert.Fail("There are less \"Add to cart\" buttons than expected");
            await addToCartButtonList.Nth(buttonIndex).ClickAsync();
        }

        public async Task ClickRemoveButton(int buttonIndex)
        {
            var removeButtonList = RemoveButtonList;
            if (removeButtonList == null)
                Assert.Fail("No \"Remove\" buttons are found");
            if (await removeButtonList.CountAsync() - 1 < buttonIndex)
                Assert.Fail("There are less \"Remove\" buttons than expected");
            await removeButtonList.Nth(buttonIndex).ClickAsync();
        }
    }
}

