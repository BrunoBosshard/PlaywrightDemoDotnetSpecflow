
using PlaywrightDemoDotnetSpecflow.Drivers;
using FluentAssertions;

namespace PlaywrightDemoDotnetSpecflow.StepDefinitions
{
    [Binding]
    public class AirportLookUpStepDefinitions : PageTest
    {
        private readonly Driver _driver;
        private readonly IPage _page;
        private readonly ILocator _searchInput;
        private readonly ILocator _searchButton;

        public AirportLookUpStepDefinitions(Driver driver)
        {
            _driver = driver;
            _page = _driver.Page;
            _searchInput = _page.Locator("input[id=\"searchbox_input\"]");
            _searchButton = _page.Locator("(//button[@aria-label='Search'])[1]");
        }

        [Given(@"I navigate to the DuckDuckGo search home page")]
        public async Task GivenIAmOnTheDuckDuckGoSearchHomePage()
        {
            int intCounter = 0;
            while (intCounter < 10)
            {
                await _page.GotoAsync("https://duckduckgo.com/");
                await _page.WaitForLoadStateAsync();
                if (await _searchInput.IsVisibleAsync())
                {
                    break;
                }
                else
                {
                    Thread.Sleep(3000);
                    intCounter++;
                }
            }
        }

        [When(@"I enter iata:""([^""]*)"" as search term")]
        public async Task WhenIEnterIataAsSearchTerm(string airportCode)
        {
            await _searchInput.FillAsync("iata:" + airportCode);
        }

        [When(@"I click on the search the web button")]
        public async Task WhenIClickOnTheSearchTheWebButton()
        {
            await _searchButton.ClickAsync();
            await _page.WaitForLoadStateAsync();
        }

        [Then(@"I should get a page containing the term ""([^""]*)""")]
        public async Task ThenIShouldGetAPageContainingTheTerm(string airportName)
        {
            string bodyText = await _page.InnerTextAsync("body");
            bodyText.ToLower().Should().Contain(airportName.ToLower());
        }
    }
}