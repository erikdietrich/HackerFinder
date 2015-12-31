Feature: RepoSearch

Scenario: Get repos for user
	When I do a repo search for user erikdietrich
	Then I should have 15 repos
	And First repo should have the following properties
	| Name                | Url                                                 | Language   | Download                                                               | Sha                                      | FileCount |
	| ASPNETWebAPISamples | https://github.com/erikdietrich/ASPNETWebAPISamples | JavaScript | https://github.com/erikdietrich/ASPNETWebAPISamples/archive/master.zip | ef4ee82d94f1d07b12f3b3981fbfef573ae9fd72 | 796       |