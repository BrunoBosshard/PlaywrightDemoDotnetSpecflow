
using PlaywrightDemoDotnetSpecflow.Drivers;
using System.Text.RegularExpressions;

namespace PlaywrightDemoDotnetSpecflow.StepDefinitions
{
    [Binding]
    public class GetStartedWithPlaywrightStepDefinitions : PageTest
    {
        private readonly Driver _driver;
        private readonly IPage _page;
        private readonly ILocator _getStartedButton;

        public GetStartedWithPlaywrightStepDefinitions(Driver driver)
        {
            _driver = driver;
            _page = _driver.Page;
            _getStartedButton = _page.Locator("text=Get Started");
        }

        [Given(@"I navigate to the Playwright home page")]
        public async Task GivenINavigateToThePlaywrightHomePage()
        {
            await _page.GotoAsync("https://playwright.dev");
        }

        [When(@"I click on the GET STARTED button")]
        public async Task WhenIClickOnTheGETSTARTEDButton()
        {
            await _getStartedButton.ClickAsync();
            await _page.WaitForLoadStateAsync();
        }

        [Then(@"I expect the URL to contain intro")]
        public async Task ThenIExpectTheURLToContainIntro()
        {
            await Expect(_page).ToHaveURLAsync(new Regex(".*intro"));
        }
    }
}