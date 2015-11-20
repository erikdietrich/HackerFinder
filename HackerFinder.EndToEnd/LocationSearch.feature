Feature: LocationSearch
	

Scenario: Wheeling Search, first name
	When I supply location Wheeling,IL
	Then I should have a user named Erik

Scenario: Wheeling Search, last name
	When I supply location Wheeling,IL
	Then I should have a user with last name Dietrich

Scenario: Wheeling Search, email address
	When I supply location Wheeling,IL
	Then I should have a user with email address erik@daedtech.com
