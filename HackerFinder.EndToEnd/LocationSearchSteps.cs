using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using System;

namespace HackerFinder.EndToEnd
{
    [Binding]
    public class LocationSearchSteps
    {
        private static IList<HackerFinder.Domain.Profile> ReturnedProfiles {  get { return ScenarioContext.Current.Get<IList<HackerFinder.Domain.Profile>>(); } }

        [When(@"I supply location (.*)")]
        public void WhenISupplyLocation(string locationText)
        {
            var inquisitor = new GithubInquisitor("erikdietrich", Environment.GetEnvironmentVariable("GithubPass", EnvironmentVariableTarget.User));
            var searcher = new ProfileSearcher(inquisitor);
            var profiles = searcher.GetProfilesForLocation(locationText);

            ScenarioContext.Current.Set(profiles);
        }
        
        [Then(@"I should have a user named (.*)")]
        public void ThenIShouldHaveAUserNamed(string firstName)
        {
            Assert.IsTrue(ReturnedProfiles.Any(pr => pr.FirstName == firstName));
        }

        [Then(@"I should have a user with last name (.*)")]
        public void ThenIShouldHaveAUserWithLastNameDietrich(string lastName)
        {
            Assert.IsTrue(ReturnedProfiles.Any(pr => pr.LastName == lastName));
        }

        [Then(@"I should have a user with email address (.*)")]
        public void ThenIShouldHaveAUserWithEmailAddressErikDaedtech_Com(string emailAddress)
        {
            Assert.IsTrue(ReturnedProfiles.Any(p => p.EmailAddress == emailAddress));
        }

        [Then(@"I should have a user with profile url ""(.*)""")]
        public void ThenIShouldHaveAUserWithProfileUrl(string expectedProfileUrl)
        {
            Assert.IsTrue(ReturnedProfiles.Any(p => p.ProfileUrl == expectedProfileUrl));
        }

    }
}
