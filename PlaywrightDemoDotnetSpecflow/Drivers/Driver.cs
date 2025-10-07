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
            // Browser
            _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });
            // Page
            return await _browser.NewPageAsync(new BrowserNewPageOptions
            {
                RecordVideoDir = "videos/",
                RecordVideoSize = new RecordVideoSize() { Width = 1024, Height = 768 },
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/141.0.0.0 Safari/537.36"
            });
        }
    }
}