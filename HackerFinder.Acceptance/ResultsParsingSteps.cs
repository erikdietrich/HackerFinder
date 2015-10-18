using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace HackerFinder.Acceptance
{
    [Binding]
    public class ResultsParsingSteps
    {
        private static ScenarioContext Current { get { return ScenarioContext.Current; } }

        [When(@"I parse the json query string (.*)")]
        public void WhenIParseTheJsonQueryStringFalseFalse(string jsonText)
        {
            var parser = new ResultsParser();
            var results = parser.ConvertToProfiles(jsonText);

            Current.Set<IEnumerable<Profile>>(results);
        }
        
        [Then(@"the result should contain a single profile")]
        public void ThenTheResultShouldContainASingleProfile()
        {
            var results = Current.Get<IEnumerable<Profile>>();

            Assert.AreEqual<int>(1, results.Count());
        }
    }
}
