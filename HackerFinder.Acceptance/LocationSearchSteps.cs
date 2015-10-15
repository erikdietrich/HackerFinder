using System;
using TechTalk.SpecFlow;
using Telerik.JustMock;
using System.Collections.Generic;
using System.Linq;
using Telerik.JustMock.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HackerFinder.Acceptance
{
    [Binding]
    public class LocationSearchSteps
    {
        private static ScenarioContext Current { get { return ScenarioContext.Current; } }

        [When(@"I supply location (.*)")]
        public void WhenISupplyLocationWheelingIL(string locationText)
        {
            var mockInquisitor = Mock.Create<IGithubInquisitor>();
            mockInquisitor.Arrange(inquisitor => inquisitor.ExecuteUrlQuery(Arg.AnyString)).Returns("erikdietrich");

            var target = new ProfileSearcher(mockInquisitor);

            var profiles = target.GetProfilesForLocation(locationText);

            Current.Set(profiles);
        }

        [Then(@"I should have a user named (.*)")]
        public void ThenIShouldHaveAUserNamedErik(string firstName)
        {
            var profiles = Current.Get<IList<Profile>>();

            Assert.IsTrue(profiles.Any(p => p.FirstName == firstName));

        }
    }
}
