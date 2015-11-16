Feature: LocationSearch
	

Scenario: Chicago Search, first name only
	When I supply location Wheeling,IL
	Then I should have a user named Erik

Scenario: Chicago Search, email only
	When I supply location Wheeling,IL
	Then I should have a user with email address erik@daedtech.com