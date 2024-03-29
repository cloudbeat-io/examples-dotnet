using CloudBeat.Kit.NUnit;
using CloudBeat.Kit.NUnit.Attributes;
using CloudBeat.Kit.Playwright;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CbExamples.NUnit.Playwright.Tests
{
    [Parallelizable(ParallelScope.Self)]
    [CbNUnitTest]
    public class Tests : PageTest
    {
        IPage _page { get; set; }

        [SetUp]
        public void Init()
        {
            _page = new CbPageWrapper(Page, CbNUnit.Current.Reporter, TestContext.CurrentContext.Test.ID);
        }

        [Test]
        public async Task HomepageHasPlaywrightInTitleAndGetStartedLinkLinkingtoTheIntroPage()
        {
            await _page.GotoAsync("https://playwright.dev");

            // Expect a title "to contain" a substring.
            var basePage = ((CbPageWrapper)_page).GetBasePage();
            var assert = Expect(basePage);
            await assert.ToHaveTitleAsync(new Regex("Playwright"));

            // create a locator
            var getStarted = _page.Locator("text=Get Started");

            // Expect an attribute "to be strictly equal" to the value.
            await Expect(((CbLocatorWrapper)getStarted).GetBaseLocator()).ToHaveAttributeAsync("href", "/docs/intro");

            // Click the get started link.
            await getStarted.ClickAsync();

            // Expects the URL to contain intro.
            await Expect(((CbPageWrapper)_page).GetBasePage()).ToHaveURLAsync(new Regex(".*intro"));

            var childElem = _page.GetByTitle("Direct link to Introduction");
            var el = _page.Locator("text=Introduction", new PageLocatorOptions { Has = ((CbLocatorWrapper)childElem).GetBaseLocator() });
        }

        public override BrowserNewContextOptions ContextOptions()
        {
            return new BrowserNewContextOptions()
            {
                ColorScheme = ColorScheme.Light,
                ViewportSize = new()
                {
                    Width = 1600,
                    Height = 900
                }
            };
        }
    }
}

