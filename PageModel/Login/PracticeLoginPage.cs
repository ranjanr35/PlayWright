using Microsoft.Playwright;

namespace ICP_Automation_Project
{
    public class PracticeLoginPage : BasePage
    {
        #region WaitForPageToLoadSuccesssfully
        public async Task<bool> WaitForPageToLoadSuccesssfully(
            IPage page,
            bool throwPageLoadException = false,
            bool refreshPage = true
        )
        {
            _logger.LogDebug("Wait for Patient Page to load successfully");
            if (refreshPage)
            {
                await page.WaitForLoadStateAsync(
                    LoadState.Load,
                    new PageWaitForLoadStateOptions { Timeout = timeout * 1000 }
                );
                await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
                await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
                RefreshPage(page);
            }

            return await Utilities.GetPageTitle(page) == ("Nextech - Login");
        }
        #endregion

        #region SelectPractice
        public async Task SelectPractice(IPage page)
        {
            _logger.LogDebug("Selecting practice");
            await Utilities.SelectOption(
                page,
                Helper.GetID("PracticeDropdown"),
                Helper.GetValue("PracticeDropdown")
            );
        }
        #endregion

        #region SelectLocation
        public async Task SelectLocation(IPage page)
        {
            _logger.LogDebug("Selecting location");
            await Utilities.SelectOption(
                page,
                Helper.GetID("LocationDropdown"),
                Helper.GetValue("LocationDropdown")
            );
        }
        #endregion

        #region SelectDepartment
        public async Task SelectDepartment(IPage page)
        {
            _logger.LogDebug("Selecting department");
            await Utilities.SelectOption(
                page,
                Helper.GetID("DepartmentDropdown"),
                Helper.GetValue("DepartmentDropdown")
            );
        }
        #endregion

        #region PerformPracticeLogin
        public async Task<WorkFlowPage> PerformPracticeLogin(IPage page)
        {
            _logger.LogDebug("Perform practice login");
            await SelectLocation(page!);
            await SelectDepartment(page!);
            await page.GetByRole(AriaRole.Button, new() { Name = Helper.GetID("SubmitButton") })
                .ClickAsync();
            return new WorkFlowPage();
        }
        #endregion
    }
}
