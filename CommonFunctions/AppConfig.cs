using Microsoft.Playwright;
using Newtonsoft.Json;

namespace Utils
{
    public partial class AppConfig
    {
        // [JsonProperty("username")]
        // public string Username { get; set; }

        // [JsonProperty("password")]
        // public string Password { get; set; }

        [JsonProperty("browserOptions")]
        public BrowserTypeLaunchOptions BrowserOptions { get; set; }

        [JsonProperty("browserContextOptions")]
        public BrowserNewContextOptions BrowserContextOptions { get; set; }

        [JsonProperty("tracingStartOptions")]
        public TracingStartOptions TracingStartOptions { get; set; }

        [JsonProperty("tracingStopOptions")]
        public TracingStopOptions TracingStopOptions { get; set; }
    }
}
