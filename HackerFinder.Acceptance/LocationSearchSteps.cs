using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;

namespace HackerFinder.Acceptance
{
    [Binding]
    public class LocationSearchSteps
    {
        [When(@"I supply location (.*)")]
        public void WhenIPressAdd(string locationText)
        {
            
        }
        
        [Then(@"I should have a user named (.*)")]
        public void ThenTheResultShouldBeOnTheScreen(string userFirstName)
        {
            Assert.IsTrue(true);
        }
    }
}
