using Microsoft.Playwright;
using AventStack.ExtentReports;

namespace ICP_Automation_Project
{
    public static class Utilities
    {
        #region PageNavigation
        public static async Task PageNavigation(IPage page, string ICPUrl, string PageName)
        {
            await page.GotoAsync(Helper.GetID(ICPUrl));
            page.Dialog += (_, dialog) => dialog.AcceptAsync();
            //await page!.WaitForURLAsync(Helper.GetValue(ICPUrl));
            BasePlaywrightTest.test!.Log(
                Status.Pass,
                "Navigated Successfully to : " + PageName ?? string.Empty
            );
        }
        #endregion

        #region PerformClickAction
        public static async Task PerformClickAction(IPage page, string locater, string FieldName)
        {
            //_logger.LogDebug("Perform click action");
            await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            await page.Locator(Helper.GetID(locater)).ClickAsync();
            BasePlaywrightTest.test!.Log(Status.Pass, "Clicked Successfully on : " + FieldName);
        }
        #endregion

        #region SelectOption
        public static async Task SelectOption(IPage page, string locater, string FieldName)
        {
            //_logger.LogDebug("Perform select option");
            await page.SelectOptionAsync(locater, FieldName);
            BasePlaywrightTest.test!.Log(Status.Pass, "Select Option selected as : " + FieldName);
        }
        #endregion

        #region CaptureText
        public static async Task<string> CaptureText(IPage page, string locator)
        {
            //_logger.LogDebug("Captiring text");
            var Textelement = await page.WaitForSelectorAsync(Helper.GetID(locator));
            var TextValue = await Textelement.GetAttributeAsync("title");
            BasePlaywrightTest.test.Log(Status.Pass, "Text Captured as : " + TextValue);
            return TextValue;
        }
        #endregion

        #region Validationforexistenceofvalue
        public static async Task Validationforexistenceofvalue(IPage page, string TextValue)
        {
            //_logger.LogDebug("Asserting for existence of value");
            Assert.IsNotNull(TextValue);
            BasePlaywrightTest.test.Log(Status.Pass, "Text verified as : " + TextValue);
        }
        #endregion

        #region ValidationforequalComparision
        public static async Task ValidationforequalComparision(
            IPage page,
            string firstValue,
            string secondValue
        )
        {
            //_logger.LogDebug("Perform validation of equal comparision");
            Assert.AreEqual(firstValue, secondValue);
            BasePlaywrightTest.test.Log(Status.Pass, "Text verified as : " + firstValue);
        }
        #endregion

        #region PressKeyboardbutton
        public static async Task PressKeyboardbutton(IPage page, string locater, string buttonName)
        {
            //_logger.LogDebug("Perform keyboard operations");
            await page.PressAsync(Helper.GetID(locater), buttonName);
            BasePlaywrightTest.test.Log(Status.Pass, buttonName + " button Pressed successfully");
        }

        public static async Task ReportLogger(
            IPage page,
            ExtentTest test,
            Status status,
            String log
        )
        {
            //_logger.LogDebug("Perform report logging");
            var title = await page.TitleAsync();
            string logdetail = title = ":" + log;
            test.Log(status, logdetail);
        }
        #endregion

        #region IsElementExists
        public static async Task<bool> IsElementExists(IPage page, string elementName)
        {
            //_logger.LogDebug("Check element existence");
            return page.Locator(Helper.GetID(elementName)).IsVisibleAsync() != null;
        }
        #endregion

        #region GetPageTitle
        public static async Task<string> GetPageTitle(IPage page)
        {
            //_logger.LogDebug("Capturing page title");
            var title = await page.TitleAsync();
            return title;
        }
        #endregion
    }
}
