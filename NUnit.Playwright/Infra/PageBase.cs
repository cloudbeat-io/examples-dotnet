using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using CloudBeat.Kit.Playwright;
using CloudBeat.Kit.NUnit;
using CloudBeat.Kit.NUnit.Attributes;

namespace CbExamples.NUnitPlaywright.Infra
{
    [CbNUnitTest]
    [TestFixture]
    public class PageBase
    {
        IPlaywright playwright;
        IBrowser browser;
        public IPage Page { get; protected set; }

        [SetUp]
        public async Task SetUpPlaywrightAsync()
        {
            playwright = await Playwright.CreateAsync();
            browser = await playwright.Chromium.LaunchAsync();
            var page = await browser.NewPageAsync(new BrowserNewPageOptions
            {
                RecordVideoDir = "videos/"
            });
            // Wrap page with CloudBeat wrapper, if the test is running on CB agent
            if (CbNUnit.IsRunningFromCB())
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

                if (TestContext.CurrentContext.Result.FailCount > 0)
                {
                    var videoPath = await Page.Video.PathAsync();
                    CbNUnit.AddScreenRecordingFromPath(videoPath);
                }

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

