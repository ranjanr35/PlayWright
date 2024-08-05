namespace ICP_Automation_Project
{
    using Serilog;

    public class Logger
    {
        private readonly string TestName;
        private readonly string TestEnvironment;

        /*public Logger()
        {
            TestName = TestContext.CurrentContext.Test.Name ?? string.Empty;
            TestEnvironment = Env.GetValue(Variables.Environment).ToUpper();
        }*/

        // Setting log when Status is Pass in LogFile and extent report
        public void LogPass(string message)
        {
            Log.Information($"[{TestEnvironment}] [{TestName}]: {message}");
        }

        // Setting log when Status is Fail in LogFile and extent report
        public void LogFail(string message)
        {
            Log.Information($"[{TestEnvironment}] [{TestName}]: {message}");
        }

        // Setting log when Status is Information in LogFile and extent report
        public void LogInformation(string message, int? testStepNumber = null)
        {
            // Conditional appending of testStepNumber if provided
            string testStepText = testStepNumber.HasValue
                ? $"Test Step: {testStepNumber} - "
                : string.Empty;

            Log.Information($"[{TestEnvironment}] [{TestName}]: {testStepText}{message}");
        }

        // Setting log when Status is Debug in LogFile and extent report
        public void LogDebug(string message)
        {
            Log.Debug($"[{TestEnvironment}] [{TestName}]: {message}");
        }

        // Setting log when Status is Warning in LogFile and extent report
        public void LogWarning(string message)
        {
            Log.Warning($"[{TestEnvironment}] [{TestName}]: {message}");
        }

        // Setting log when Status is Error in LogFile and extent report
        public void LogError(Exception exception, string message)
        {
            Log.Error(exception, $"[{TestEnvironment}] [{TestName}]: {message}");
        }

        public void LogError(string message)
        {
            Log.Error($"[{TestEnvironment}] [{TestName}]: {message}");
        }

        // Setting log when Status is Fatal in LogFile and extent report
        public void LogFatal(string message)
        {
            Log.Fatal($"[{TestEnvironment}] [{TestName}]: {message}");
        }

        // Setting log when Status is Skip in LogFile and extent report
        public void LogSkip(string message)
        {
            Log.Information($"[{TestEnvironment}] [{TestName}]: {message}");
        }

        // Setting log when Status is Verbose in LogFile and extent report
        public void LogVerbose(string message)
        {
            Log.Verbose($"[{TestEnvironment}] [{TestName}]: {message}");
        }
    }
}
