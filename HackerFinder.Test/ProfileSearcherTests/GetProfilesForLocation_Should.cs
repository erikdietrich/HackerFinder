using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerFinder.Test.ProfileSearcherTests
{
    [TestClass]
    public class GetProfilesForLocation_Should
    {
        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Return_Profile_With_FirstName_Set_To_Erik()
        {
            var target = new ProfileSearcher();

            var firstProfile = target.GetProfilesForLocation("Wheeling,IL").First();

            Assert.AreEqual<string>("Erik", firstProfile.FirstName);
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Return_EmptyEnumeration_For_Nonsense()
        {
            var target = new ProfileSearcher();

            var profiles = target.GetProfilesForLocation("7ea52ac5-5fe4-4ad5-9aa1-4a994d60c95e");

            Assert.AreEqual<int>(0, profiles.Count());
        }
    }
}
