Feature: LocationSearch
	

Scenario: Chicago Search, first name
	When I supply location Wheeling,IL
	Then I should have a user named Erik

Scenario: Chicago Search, email address
	When I supply location Wheeling,IL
	Then I should have a user with email address erik@daedtech.com
