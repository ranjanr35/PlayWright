using Utils;
using Microsoft.Playwright;

namespace ICP_Automation_Project
{
    public class BrowserUtility
    {
        static IPage? page;
        public static AppConfig? appConfig;
        public static IBrowser? browser;
        public static IBrowserContext? context;

        // Opening Chrome Browser
        public async static Task<IPage> SetupWebDriver()
        {
            var browserType = "chrome";
            switch (browserType)
            {
                case "chrome":
                    var driver = await Playwright.CreateAsync();

                    browser = await driver.Chromium.LaunchAsync(appConfig!.BrowserOptions);

                    context = await browser.NewContextAsync(appConfig.BrowserContextOptions);
                    await context.Tracing.StartAsync(
                        new TracingStartOptions { Screenshots = true, Snapshots = true }
                    );
                    page = await context.NewPageAsync().ConfigureAwait(false);
                    break;

                case "edge":
                    driver = await Playwright.CreateAsync();

                    browser = await driver.Chromium.LaunchAsync(appConfig!.BrowserOptions);

                    context = await browser.NewContextAsync(appConfig.BrowserContextOptions);
                    await context.Tracing.StartAsync(
                        new TracingStartOptions { Screenshots = true, Snapshots = true }
                    );
                    page = await context.NewPageAsync().ConfigureAwait(false);
                    break;

                case "firefox":
                    driver = await Playwright.CreateAsync();

                    browser = await driver.Chromium.LaunchAsync(appConfig!.BrowserOptions);

                    context = await browser.NewContextAsync(appConfig.BrowserContextOptions);
                    await context.Tracing.StartAsync(
                        new TracingStartOptions { Screenshots = true, Snapshots = true }
                    );
                    page = await context.NewPageAsync().ConfigureAwait(false);
                    break;
                default:
                    throw new ArgumentException(
                        $"Invalid browser type was specified: {browserType}"
                    );
            }
            return page!;
        }
    }
}
