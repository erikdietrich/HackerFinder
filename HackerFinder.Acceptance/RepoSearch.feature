Feature: RepoSearch

Scenario: Get repo for a user
	When I do a repo search for user erikdietrich
	Then I should have 15 repos
	And First repo should have the following properties
	| Name                | Url                                                 | Language   | Download                                                               |
	| ASPNETWebAPISamples | https://github.com/erikdietrich/ASPNETWebAPISamples | JavaScript | https://github.com/erikdietrich/ASPNETWebAPISamples/archive/master.zip |
