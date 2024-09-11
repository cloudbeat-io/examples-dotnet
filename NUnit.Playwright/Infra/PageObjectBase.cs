using Microsoft.Playwright;

namespace CbExamples.NUnitPlaywright.Infra
{
    public class PageObjectBase
    {
        protected readonly IPage page;

        public PageObjectBase(IPage page)
		{
			this.page = page;
		}
	}
}

