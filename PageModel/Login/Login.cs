using Microsoft.Playwright;

namespace ICP_Automation_Project
{
    public class Login : BasePage
    {
        #region PerformLogin
        public static async Task<PracticeLoginPage> PerformLogin(IPage page)
        {
            //_logger.LogDebug("Perform login operation");
            Assert.IsTrue(await WaitForPageToLoadSuccesssfully(page!));
            await page!.Locator(Helper.GetID("PracticeName")).ClickAsync();
            await page.Locator(Helper.GetID("PracticeName"))
                .FillAsync(Helper.GetValue("PracticeName"));
            await page.Locator(Helper.GetID("PracticeName")).PressAsync("Tab");
            await page.Locator(Helper.GetID("UserName")).FillAsync(Helper.GetValue("UserName"));
            await page.Locator(Helper.GetID("UserName")).PressAsync("Tab");
            await page.Locator(Helper.GetID("Password")).FillAsync(Helper.GetValue("Password"));
            await Utilities.IsElementExists(page, Helper.GetID("Password"));
            await page.GetByRole(AriaRole.Button, new() { Name = Helper.GetID("LoginButton") })
                .ClickAsync();
            return new PracticeLoginPage();
        }
        #endregion

        #region WaitForPageToLoadSuccesssfully
        public static async Task<bool> WaitForPageToLoadSuccesssfully(
            IPage page,
            bool throwPageLoadException = false,
            bool refreshPage = true
        )
        {
            //_logger.LogDebug("Wait for Patient Page to load successfully");
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

            if (await page.Locator(Helper.GetID("LoadingIndicator")).IsVisibleAsync())
            {
                await page.Locator(Helper.GetID("LoadingIndicator")).IsDisabledAsync();
            }

            await page.WaitForLoadStateAsync(
                LoadState.Load,
                new PageWaitForLoadStateOptions { Timeout = timeout * 1000 }
            );
            await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            return await Utilities.GetPageTitle(page) == ("Nextech - Login");
        }
        #endregion
    }
}
