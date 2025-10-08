namespace PlaywrightDemoDotnetSpecflow.Drivers
{
    public class Driver : IDisposable
    {
        private readonly Task<IPage> _page;
        private IBrowser? _browser;

        public Driver() => _page = Task.Run(InitializePlaywright);

        public IPage Page => _page.Result;

        public void Dispose() => _browser?.CloseAsync();

        private async Task<IPage> InitializePlaywright()
        {
            // Playwright
            var playwright = await Playwright.CreateAsync();
            
            // Detect if running in CI environment
            var isCI = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("CI")) || 
                      !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("GITHUB_ACTIONS"));
            
            // Browser configuration optimized for CI environments
            var browserOptions = new BrowserTypeLaunchOptions
            {
                Headless = isCI, // Use headless in CI, headed locally
                Args = isCI ? new[] 
                {
                    "--no-sandbox",
                    "--disable-setuid-sandbox",
                    "--disable-dev-shm-usage",
                    "--disable-web-security",
                    "--disable-features=VizDisplayCompositor",
                    "--disable-background-timer-throttling",
                    "--disable-backgrounding-occluded-windows",
                    "--disable-renderer-backgrounding"
                } : new string[0]
            };
            
            _browser = await playwright.Chromium.LaunchAsync(browserOptions);
            
            // Page configuration
            var pageOptions = new BrowserNewPageOptions
            {
                UserAgent = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36"
            };
            
            // Only record videos locally, not in CI
            if (!isCI)
            {
                pageOptions.RecordVideoDir = "videos/";
                pageOptions.RecordVideoSize = new RecordVideoSize() { Width = 1024, Height = 768 };
            }
            
            var page = await _browser.NewPageAsync(pageOptions);
            
            // Set longer timeouts for CI environments
            if (isCI)
            {
                page.SetDefaultTimeout(60000); // 60 seconds
                page.SetDefaultNavigationTimeout(60000); // 60 seconds
            }
            
            return page;
        }
    }
}