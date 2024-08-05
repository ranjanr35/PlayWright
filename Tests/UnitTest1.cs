using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Utils;

namespace ICP_Automation_Project;

[TestClass]
public class ExampleTest : BasePlaywrightTest
{
    #region ClassInitialize
    [ClassInitialize]
    public static async Task ClassInitialize(TestContext Context)
    {
        Console.WriteLine("Class Initialise started");
        await ClassInit(Context);
    }
    #endregion

    #region TC01_SearchPatient
    [TestMethod]
    public async Task TC01_SearchPatient()
    {
        _logger!.LogInformation("Initialize page object for workflow page");
        var _workflowPage = new WorkFlowPage();

        _logger!.LogInformation("Assertion for workflow page to be loaded successfully");
        Assert.IsTrue(
            await _workflowPage!.WaitForPageToLoadSuccesssfully(page!),
            "Failed to load workflow page"
        );

        _logger!.LogInformation("Perform patient search and select patient");
        var _patientSummaryPage = await _workflowPage.PerformPatientSearchAndSelectThePatient(
            page!,
            Helper.GetValue("PatientSearchTextBox")
        );

        _logger!.LogInformation("Assertion for patient summary page to be loaded successfully");
        Assert.IsTrue(
            await _patientSummaryPage.WaitForPageToLoadSuccesssfully(page!),
            "Failed to load patient summary page"
        );
    }
    #endregion

    #region TC02_VerifyIntelleChartProPage
    [TestMethod]
    public async Task TC02_VerifyIntelleChartProPage()
    {
        _logger!.LogInformation("Initialize page object for workflow page");
        var _workflowPage = new WorkFlowPage();

        _logger!.LogInformation("Assertion for workflow page to be loaded successfully");
        Assert.IsTrue(
            await _workflowPage!.WaitForPageToLoadSuccesssfully(page!),
            "Failed to load workflow page"
        );

        _logger!.LogInformation("Perform patient search and select patient");
        var _patientSummaryPage = await _workflowPage.PerformPatientSearchAndSelectThePatient(
            page!,
            Helper.GetValue("PatientSearchTextBox")
        );

        _logger!.LogInformation("Assertion for patient summary page to be loaded successfully");
        Assert.IsTrue(
            await _patientSummaryPage.WaitForPageToLoadSuccesssfully(page!),
            "Failed to load patient summary page"
        );

        _logger!.LogInformation("Navigating to IntelleChartPro page");
        var _intelleChartProPage = await _patientSummaryPage.NavigateToIntelleChartPro(page!);

        _logger!.LogInformation("Assertion for IntelleChartPro page to be loaded successfully");
        Assert.IsTrue(
            await _intelleChartProPage.WaitForPageToLoadSuccesssfully(page!),
            "Failed to load patient summary page"
        );
    }
    #endregion
}
