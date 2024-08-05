using Microsoft.Playwright;

namespace ICP_Automation_Project
{
    public class WorkFlowPage : BasePage
    {
        #region WaitForPageToLoadSuccesssfully
        public async Task<bool> WaitForPageToLoadSuccesssfully(IPage page)
        {
            _logger!.LogDebug("Wait for Patient Page to load successfully");
            if (await page.Locator(Helper.GetID("LoadingIndicator")).IsVisibleAsync())
            {
                await page.Locator(Helper.GetID("LoadingIndicator")).IsDisabledAsync();
            }
            await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            ClosePopupWindow(page);
            await Task.Delay(30000);
            return await page.Locator(Helper.GetID("PatientSearchText")).IsVisibleAsync();
        }
        #endregion

        #region ClosePopupWindow
        public async void ClosePopupWindow(IPage page)
        {
            _logger!.LogDebug("Closing popup window");
            await page.Locator(Helper.GetID("ClosePopupWindow")).ClickAsync();
        }
        #endregion

        #region PerformPatientSearchAndSelectThePatient
        public async Task<PatientSummaryPage> PerformPatientSearchAndSelectThePatient(
            IPage page,
            string patientName
        )
        {
            _logger!.LogDebug("Searching and selecting patient");
            try
            {
                await SearchAndSelectThePatient(page, patientName);
            }
            catch
            {
                _logger!.LogInformation(
                    "Refreshing page and performing patient search if any pop-up appears in first attempt"
                );
                RefreshPage(page);
                await WaitForPageToLoadSuccesssfully(page);
                await SearchAndSelectThePatient(page, patientName);
            }
            return new PatientSummaryPage();
        }
        #endregion

        #region SearchAndSelectThePatient
        public async Task SearchAndSelectThePatient(IPage page, string patientName)
        {
            IPage _patientSummaryPage = null;

            _logger!.LogDebug(
                "Entering text in patient search input field and if any popup comes closing it"
            );
            await page.Locator(Helper.GetID("PatientSearchTextBox")).ClearAsync();
            await page.Locator(Helper.GetID("PatientSearchTextBox")).ClickAsync();
            await page.Locator(Helper.GetID("PatientSearchTextBox")).FillAsync(patientName);

            _logger.LogDebug("Waiting for Patient search modal");
            if (await Utilities.IsElementExists(page, "PatientSearchResult!"))
            {
                await page.Locator(Helper.GetID("PatientSearchIcon")).ClickAsync();
            }
            else
            {
                await page.Locator(Helper.GetID("PatientSearchIcon")).ClickAsync();
            }

            _logger.LogDebug("Selecting searched patient in Patient search modal");
            if (await Utilities.IsElementExists(page, "PatientSearchResult!"))
            {
                await page.RunAndWaitForPopupAsync(async () =>
                {
                    await page.GetByRole(AriaRole.Cell, new() { Name = patientName }).ClickAsync();
                });
            }
        }
        #endregion
    }
}
