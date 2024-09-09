using System;
using System.Collections.Generic;
using CloudBeat.Kit.NUnit;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Events;
using WebDriverManager.DriverConfigs.Impl;

namespace CbExamples.NUnit.Infra
{
    public class WebDriverTest : TestBase
    {
        private IWebDriver _driver = null;

        public EventFiringWebDriver Driver { get; private set; }

        [SetUp]
        public void SetUpWebDriver()
        {
            if (CbNUnit.Current.Config.HasMandatory())
                SetupRemoteChromeDriver();
            else
                SetupLocalChromeDriver();
        }

        private void SetupLocalChromeDriver()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            _driver = new ChromeDriver();
            Driver = new EventFiringWebDriver(_driver);
            CbNUnit.WrapWebDriver(Driver);
        }

        private void SetupRemoteChromeDriver()
        {
            // string hubUrl = "http://ec2-3-78-227-222.eu-central-1.compute.amazonaws.com:4444/wd/hub";
            string localHubUrl = "http://localhost:4444/wd/hub";
            ChromeOptions options = new ChromeOptions();
            string videoFileName = TestContext.CurrentContext.Test.ID + ".mp4";
            Dictionary<string, object> selenoidOpts = new Dictionary<string, object>
            {
                { "enableVideo", true },
                { "enableVNC", true },
                { "videoName", videoFileName }
            };
            options.AddAdditionalOption("selenoid:options", selenoidOpts);
            _driver = new RemoteWebDriver(new Uri(localHubUrl), options.ToCapabilities());
            Driver = new EventFiringWebDriver(_driver);
            CbNUnit.WrapWebDriver(Driver);
        }

        [TearDown]
        public void DisposeWebDriver()
        {
            if (_driver != null)
            {
                CbNUnit.AddScreenshotOnError();

                try
                {
                    _driver.Close();
                    _driver.Quit();
                }
                catch
                {
                }

                // retrieve video from Selenoid if applicable
                /*string videoFileName = TestContext.CurrentContext.Test.ID + ".mp4";
                CbNUnit.AddScreenRecordingFromUrl(
                    $"http://ec2-3-78-227-222.eu-central-1.compute.amazonaws.com:4444/video/{videoFileName}");
                */

                _driver = null;
                Driver = null;
            }
        }
    }
}

