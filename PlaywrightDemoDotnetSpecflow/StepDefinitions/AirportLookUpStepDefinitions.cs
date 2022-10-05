
using PlaywrightDemoDotnetSpecflow.Drivers;
using FluentAssertions;

namespace PlaywrightDemoDotnetSpecflow.StepDefinitions
{
    [Binding]
    public class AirportLookUpStepDefinitions : PageTest
    {
        private readonly Driver _driver;
        private readonly IPage _page;
        private readonly ILocator _acceptCookiesButton;
        private readonly ILocator _searchInput;
        private readonly ILocator _searchButton;

        public AirportLookUpStepDefinitions(Driver driver)
        {
            _driver = driver;
            _page = _driver.Page;
            _acceptCookiesButton = _page.Locator("id=bnp_btn_accept");
            _searchInput = _page.Locator("input[name=\"q\"]");
            _searchButton = _page.Locator("id=search_icon");
        }

        [Given(@"I navigate to the Bing search home page")]
        public async Task GivenIAmOnTheBingSearchHomePage()
        {
            await _page.GotoAsync("https://www.bing.com");
            await _acceptCookiesButton.ClickAsync();
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

        [Then(@"I should get a page cotaining the term ""([^""]*)""")]
        public async Task ThenIShouldGetAPageCotainingTheTerm(string airportName)
        {
            string bodyText = await _page.InnerTextAsync("body");
            bodyText.ToLower().Should().Contain(airportName.ToLower());
        }
    }
}