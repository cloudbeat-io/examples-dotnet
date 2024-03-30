using System;
using Microsoft.Playwright;

namespace CbExamples.NUnitPlaywright.Infra
{
	public class PageObjectBase
    {
        protected static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();
        protected readonly IPage page;

        public PageObjectBase(IPage page)
		{
			this.page = page;
		}
	}
}

