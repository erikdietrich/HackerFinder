Feature: LocationSearch
	

Scenario: Wheeling Search
	When I supply location Wheeling,IL
	Then I should have a user named Erik
	And I should have a user with last name Dietrich
	And I should have a user with email address erik@daedtech.com
	And I should have a user with profile url "https://github.com/erikdietrich"
