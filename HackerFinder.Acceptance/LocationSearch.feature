Feature: LocationSearch
	

Scenario: Wheeling Search, first name only
	When I supply location Wheeling,IL
	Then I should have a user named Erik

Scenario: Wheeling Search, email only
	When I supply location Wheeling,IL
	Then I should have a user with email address erik@daedtech.com

Scenario: Wheeling search, correct number of total results
	When I supply location Wheeling,IL
	Then I should get back 9 total results