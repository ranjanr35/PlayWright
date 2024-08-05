using ICP_Automation_Project.PageModel.IntelleChartPro;
using Microsoft.Playwright;

namespace ICP_Automation_Project
{
    public class PatientSummaryPage : BasePage
    {
        #region WaitForPageToLoadSuccesssfully
        public async Task<bool> WaitForPageToLoadSuccesssfully(IPage page)
        {
            IPage _patientSummaryPage = await SwitchToTab(
                page!,
                "Asc Automation | 10/9/1974 | 49 Years | 89564"
            );
            _logger.LogDebug("Wait for Patient Page to load successfully");
            await _patientSummaryPage.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            await _patientSummaryPage.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await Task.Delay(3000);
            return await Utilities.GetPageTitle(_patientSummaryPage)
                == ("Asc Automation | 10/9/1974 | 49 Years | 89564");
        }
        #endregion

        #region NavigateToIntelleChartPro
        public async Task<IntelleChartProPage> NavigateToIntelleChartPro(IPage page)
        {
            _logger.LogDebug("Navigating to Intelle Chart Pro page");
            IPage _patientSummaryPage = await SwitchToTab(
                page!,
                "Asc Automation | 10/9/1974 | 49 Years | 89564"
            );
            await _patientSummaryPage.RunAndWaitForPopupAsync(async () =>
            {
                await _patientSummaryPage
                    .GetByRole(AriaRole.Img, new() { Name = "Open IntelleChart Pro" })
                    .ClickAsync();
            });
            return new IntelleChartProPage();
        }
        #endregion
    }
}
