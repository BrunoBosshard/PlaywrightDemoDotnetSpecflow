
using PlaywrightDemoDotnetSpecflow.Drivers;
using TechTalk.SpecFlow.Assist;

namespace PlaywrightDemoDotnetSpecflow.StepDefinitions
{
    [Binding]
    public class ZipCodeLookUpStepDefinitions : PageTest
    {
        private readonly Driver _driver;
        private readonly IPage _page;
        private readonly ILocator _cityInput;
        private readonly ILocator _stateSelect;
        private readonly ILocator _findButton;

        public ZipCodeLookUpStepDefinitions(Driver driver)
        {
            _driver = driver;
            _page = _driver.Page;
            _cityInput = _page.Locator("input[name=\"tCity-city-state\"]");
            _stateSelect = _page.Locator("select[name=\"tState-city-state\"]");
            _findButton = _page.Locator("#zip-by-city-and-state");
        }

        [Given(@"I navigate to the USPS Look Up a ZIP Code page")]
        public async Task GivenINavigateToTheUSPSLookUpAZIPCodePage()
        {
            await _page.GotoAsync("https://tools.usps.com/zip-code-lookup.htm?bycitystate");
        }

        [When(@"I enter City and State")]
        public async Task WhenIEnterCityAndState(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            await _cityInput.FillAsync((string)data.City);
            await _stateSelect.SelectOptionAsync((string)data.State);
        }

        [When(@"I click the Find button")]
        public async Task WhenIClickTheFindButton()
        {
            await _findButton.ClickAsync();
            await _page.WaitForLoadStateAsync();
        }

        [Then(@"I sould get a page containing the selector BEVERLY HILLS CA 90210")]
        public async Task ThenISouldGetAPageContainingBEVERLYHILLSCA()
        {
            // This is also an assertion
            await _page.IsEnabledAsync("text=BEVERLY HILLS CA 90210", new PageIsEnabledOptions { Timeout = 3000 });
        }
    }
}