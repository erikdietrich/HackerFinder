Feature: LocalCandidateSearch
	As a candidate searcher
	I want to find people that are local and that have repos for a particular language
	So that I can have their contact info as possible matches

Scenario: Wheeling users with C# repositories
	When I search Wheeling and C#
	Then The result should include erikdietrich