using System;
using Microsoft.Playwright;
using OpenQA.Selenium;

namespace CbExamples.NUnit.Playwright.Infra
{
	public class PageBase
    {
		protected IPage page;

        public PageBase(IPage page)
		{
			this.driver = driver;
		}
	}
}

