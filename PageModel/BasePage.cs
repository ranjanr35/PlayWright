using Microsoft.Playwright;

namespace ICP_Automation_Project
{
    public class BasePage
    {
        public static int timeout = 60000;
        public static IBrowserContext? context;
        public Logger _logger;

        public BasePage()
        {
            _logger = new Logger();
        }

        #region RefreshPage
        public static void RefreshPage(
            IPage page,
            string pageName = "",
            bool takeScreenshot = false
        )
        {
            //_logger.LogDebug("Refreshing page and waiting for page to load");
            if (takeScreenshot)
            {
                pageName = pageName.Length > 0 ? $"{pageName} " : pageName;
                /*ReportingUtility.AttachScreenshot(
                    $"Screenshot before refreshing the {pageName}page"
                );*/
            }
            page.ReloadAsync();
        }
        #endregion

        #region WaitUntilBrowserContainsNumberOfTabs
        public async Task<bool> WaitUntilBrowserContainsNumberOfTabs(int count)
        {
            _logger.LogDebug("Wait Until Browser Contains Number Of Tabs");
            if (context!.Pages.Count() == count)
            {
                return true;
            }
            return false;
        }
        #endregion

        /*#region GetCurrentTabIndex
        public async Task<int> GetCurrentTabIndex(IPage page)
        {
            var tabs = context!.Pages.ToList();
            string currentPageTitle = await Utilities.GetPageTitle(page);
            int currentTabIndex = -1;
            for (var i = 0; i < tabs.Count; i++)
            {
                //                WebDriverFactory.GetDriver().SwitchTo().Window(tabs[i]);
                if (await Utilities.GetPageTitle(page) == currentPageTitle)
                    currentTabIndex = i;
            }
            return currentTabIndex;
        }
        #endregion*/

        #region SwitchToNewTab
        public async void SwitchToNewTab()
        {
            _logger.LogDebug("Switching to new page");
            IPage _workFlowPage = await context!.NewPageAsync();
        }
        #endregion

        #region SwitchToTab
        public static async Task<IPage> SwitchToTab(IPage currentPage, string pageTitle)
        {
            //_logger.LogDebug("Switch to new tab");
            var context = currentPage.Context;
            var pages = context.Pages;

            foreach (var newPage in pages)
            {
                await Task.Delay(3000);
                var title = await newPage.TitleAsync();
                if (title == pageTitle)
                {
                    await newPage.BringToFrontAsync();
                    return newPage;
                }
            }
            return currentPage;
        }
        #endregion
    }
}
