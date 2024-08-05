using Microsoft.Playwright;

namespace ICP_Automation_Project
{
    public class WebDriverFactory
    {
        private static readonly ThreadLocal<IPage> driver = new();

        #region SetDriver
        public static async void SetDriver()
        {
            driver.Value = await BrowserUtility.SetupWebDriver();
        }
        #endregion

        #region
        public static IPage GetDriver()
        {
            if (driver.Value == null)
                throw new Exception(
                    "Please call method 'DriverManager.SetDriver()' before getting the WebDriver"
                );

            return driver.Value!;
        }
        #endregion

        #region IsDriverSetup
        public static bool IsDriverSetup()
        {
            return driver.Value != null;
        }
        #endregion
    }
}
