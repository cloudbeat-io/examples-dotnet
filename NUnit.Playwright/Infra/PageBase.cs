using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;

namespace CbExamples.NUnitPlaywright.Infra
{
	public class PageBase : TestBase
    {
        IPlaywright playwright;
        IBrowser browser;
        public IPage Page { get; protected set; }

        [SetUp]
        public async Task SetUpPlaywrightAsync()
        {
            playwright = await Playwright.CreateAsync();
            browser = await playwright.Chromium.LaunchAsync();
            var page = await browser.NewPageAsync();
            Page = page;
        }

        [TearDown]
        public async Task DisposePlaywrightAsync()
        {
            if (Page != null)
            {
                await Page.CloseAsync();
                Page = null;
            }
            if (browser != null)
            {
                await browser.CloseAsync();
                await browser.DisposeAsync();
                browser = null;
            }
            if (playwright != null)
            {
                playwright.Dispose();
                playwright = null;
            }
        }
    }
}

