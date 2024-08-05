using Utils;
using Microsoft.Playwright;
using AventStack.ExtentReports;
using System.Text.Json;
using System.Diagnostics;
using AventStack.ExtentReports.Reporter;

namespace ICP_Automation_Project
{
    [TestClass]
    public class BasePlaywrightTest
    {
        public static ExtentReports? extent;
        public static ExtentTest? test;
        public static IPage? page;
        public static IBrowser? browser;
        public static IBrowserContext? context;
        public static AppConfig? appConfig;
        public static string? ICPUrl;
        public static TestContext? ClassTextContext;
        static string? reportPath;
        private TestContext? testContextInstance;
        public BasePage? _basePage;
        public WorkFlowPage? _workFlowPage;
        public Logger? _logger;

        public BasePlaywrightTest()
        {
            _logger = new Logger();
        }

        #region killAllChromeProcess
        static void killAllChromeProcess()
        {
            //_logger.LogDebug("Closing all chrome sessions");
            Process[] ps = Process.GetProcessesByName("chrome");
            foreach (Process p in ps)
                p.Kill();
        }
        #endregion

        #region LoadConfig
        private static void LoadConfig()
        {
            //_logger.LogDebug("Read Config file to get BrowserTypeLaunchOptions,BrowserNewContextOptions,Tracing Options");
            String s = File.ReadAllText("playwright.config");
            appConfig = JsonSerializer.Deserialize<AppConfig>(
                s,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
        }
        #endregion

        #region CloseChrome
        private static void CloseChrome()
        {
            //_logger.LogDebug("Close previously open browser instances");
            Process[] chromeInstances = Process.GetProcessesByName("chrome");

            foreach (Process p in chromeInstances)
                p.Kill();
        }
        #endregion

        #region StartExtentReports
        private static void StartExtentReports()
        {
            //_logger.LogDebug("Starting extent reports");
            extent = new ExtentReports();
            var path = System.Reflection.Assembly.GetCallingAssembly().Location;

            var actualPath = path.Substring(0, path.LastIndexOf("bin"));
            var projectPath = new Uri(actualPath).LocalPath;
            Directory.CreateDirectory(projectPath.ToString() + "Reports");
            String timeStamp = DateTime.Now.ToLongTimeString();
            reportPath = projectPath + "Reports\\index.html";
            var htmlReporter = new ExtentSparkReporter(reportPath);
            extent.AttachReporter(htmlReporter);
        }
        #endregion

        #region TestContext
        public TestContext TestContext
        {
            get { return testContextInstance!; }
            set { testContextInstance = value; }
        }
        #endregion

        #region ClassInit
        public static async Task ClassInit(TestContext testContext)
        {
            //_logger.LogDebug("Initilizing class");
            Console.WriteLine("Starting ClassInitialise");
            File.WriteAllText("ClassInit.txt", "TestInit getting called");
            ClassTextContext = testContext;
            string testname = ClassTextContext!.TestName!;
            var driver = await Playwright.CreateAsync();

            browser = await driver.Chromium.LaunchAsync(appConfig!.BrowserOptions);

            context = await browser.NewContextAsync(appConfig.BrowserContextOptions);
            await context.Tracing.StartAsync(
                new TracingStartOptions { Screenshots = true, Snapshots = true }
            );
            page = await context.NewPageAsync().ConfigureAwait(false);
            await page.SetViewportSizeAsync(1920, 1080);
        }
        #endregion

        #region LoginToICPApplication
        public async Task LoginToICPApplication(IPage page)
        {
            var _practiceMainPage = await Login.PerformLogin(page);
            await _practiceMainPage.PerformPracticeLogin(page);
        }
        #endregion

        #region TestSuiteInit
        [AssemblyInitialize]
        public static void TestSuiteInit(TestContext testContext)
        {
            Console.WriteLine("Starting TestSuite");
            LoadConfig();
            StartExtentReports();
        }
        #endregion

        #region TestInitialise
        [TestInitialize]
        public async Task TestInitialise()
        {
            string testname = TestContext!.TestName!;
            test = extent!.CreateTest(TestContext.TestName);
            TestContext.WriteLine("Starting test:::" + TestContext.TestName);
            await Utilities.PageNavigation(page!, "ICPUrl", "ICP Login Page");

            _logger.LogDebug("Login to ICP Application");
            var _workFlowPage = LoginToICPApplication(page!);
        }
        #endregion

        #region TestCleanup
        [TestCleanup]
        public static async Task TestCleanup(TestContext testContext)
        {
            await context!.Tracing.StopAsync(appConfig!.TracingStopOptions);
            await page!.PdfAsync(
                new Microsoft.Playwright.PagePdfOptions
                {
                    Path =
                        "C:\\Users\\rranj\\OneDrive\\Documents\\Playwright_Project\\ICP_Automation_Project\\page.pdf",
                    Format = "Ledger"
                }
            );
            Console.WriteLine("Test Completed successfully");
            await browser!.CloseAsync();
            await context.ClearCookiesAsync();
            if (testContext.CurrentTestOutcome == UnitTestOutcome.Failed)

                test!.Log(Status.Fail, "test completed");
            else
                test!.Log(Status.Pass, "test completed");
        }
        #endregion

        #region TestSuiteEnd
        [AssemblyCleanup]
        public static void TestSuiteEnd()
        {
            extent!.Flush(); //
            String timeStamp = DateTime.Now.ToString("hhmmss_ddMMMyyyy");
            string newReportName = reportPath!.Replace("index", "TestReport_" + timeStamp);
            File.WriteAllText("text.txt", newReportName);
            File.Move(reportPath, newReportName);
        }
        #endregion
    }
}
