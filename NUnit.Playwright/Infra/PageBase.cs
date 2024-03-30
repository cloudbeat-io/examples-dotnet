using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using CloudBeat.Kit.Playwright;
using CloudBeat.Kit.NUnit;

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
            // Wrap page with CloudBeat wrapper, if the test is running on CB agent
            if (CbNUnit.Current.IsConfigured && CbNUnit.Current.Reporter != null)
                Page = new CbPageWrapper(page, CbNUnit.Current.Reporter);
            else
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

