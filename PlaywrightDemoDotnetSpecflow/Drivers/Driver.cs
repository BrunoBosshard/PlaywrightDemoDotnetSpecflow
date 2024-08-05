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
            _browser = await playwright.firefox.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });
            // Page
            return await _browser.NewPageAsync(new BrowserNewPageOptions
            {
                RecordVideoDir = "videos/",
                RecordVideoSize = new RecordVideoSize() { Width = 1024, Height = 768 },
                UserAgent = "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:128.0) Gecko/20100101 Firefox/128.0"
            });
        }
    }
}
