using Microsoft.Playwright;

namespace ICP_Automation_Project.PageModel.IntelleChartPro
{
    public class IntelleChartProPage : BasePage
    {
        #region WaitForPageToLoadSuccesssfully
        public async Task<bool> WaitForPageToLoadSuccesssfully(IPage page)
        {
            IPage _IntelleChartProPage = await SwitchToTab(
                page!,
                "Asc Automation | 10/9/1974 | 49 Years | 89564"
            );
            _logger.LogDebug("Wait for IntelleChartPro Page to load successfully");
            await _IntelleChartProPage.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            await _IntelleChartProPage.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await Task.Delay(30000);
            return await Utilities.GetPageTitle(_IntelleChartProPage)
                == ("Asc Automation | 10/9/1974 | 49 Years | 89564");
        }
        #endregion
    }
}
