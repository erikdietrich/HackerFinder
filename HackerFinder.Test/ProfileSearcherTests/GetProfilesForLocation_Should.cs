using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;

namespace HackerFinder.Test.ProfileSearcherTests
{
    [TestClass]
    public class GetProfilesForLocation_Should
    {
        private IGithubInquisitor Inquisitor { get; set; }

        private ProfileSearcher Target { get; set; }

        [TestInitialize]
        public void BeforeEachTest()
        {
            Inquisitor = Mock.Create<IGithubInquisitor>();
            Target = new ProfileSearcher(Inquisitor);
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Return_EmptyEnumeration_For_Nonsense()
        {
            var profiles = Target.GetProfilesForLocation("7ea52ac5-5fe4-4ad5-9aa1-4a994d60c95e");

            Assert.AreEqual<int>(0, profiles.Count());
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Return_A_Profile_With_FirstName_Erik_When_GithubInquisitor_Returns_Result_With_String_erikdietrich()
        {
            
            Inquisitor.Arrange(i => i.ExecuteUrlQuery(Arg.AnyString)).Returns("erikdietrich");

            var firstProfile = Target.GetProfilesForLocation("Wheeling,IL").First();

            Assert.AreEqual<string>("Erik", firstProfile.FirstName);
        }
    }
    
}
