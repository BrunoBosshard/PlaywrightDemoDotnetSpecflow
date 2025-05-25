Feature: Examples

Playwright Behaviour-Driven Development (BDD) examples

@smoke
Scenario: Get Started with Playwright
	Given I navigate to the Playwright home page
	When I click on the GET STARTED button
	Then I expect the URL to contain intro

@regression
Scenario: Look Up a US ZIP Code
	Given I navigate to the USPS Look Up a ZIP Code page
	When I enter City and State
	| City | State |
	| Beverly Hills | CA |
	And I click the Find button
	Then I should get a page containing the text BEVERLY HILLS CA

@regression
Scenario Outline: Airport Code Look Up
	Given I navigate to the DuckDuckGo search home page
	When I enter iata:"<airportCode>" as search term
	And I click on the search the web button
	Then I should get a page cotaining the term "<airportName>"

	Examples:
	| airportCode | airportName |
	| ATL | Atlanta |
	| DFW | Dallas |
	| DEN | Denver |
	| LAX | Los Angeles |
	| SFO | San Francisco |