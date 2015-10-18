using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;
using HackerFinder.Exceptions;

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
        public void Return_A_Profile_With_FirstName_Erik_When_GithubInquisitor_Returns_Result_With_Username_erikdietrich()
        {
            const string userName = "erikdietrich";

            Inquisitor.Arrange(i => i.ExecuteUrlQuery(Arg.AnyString)).Returns(userName);

            var firstProfile = Target.GetProfilesForLocation("Wheeling,IL").First();

            Assert.AreEqual<string>("Erik", firstProfile.FirstName);
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Return_A_Profile_With_LastName_Dietrich_When_GithubInquisitor_Returns_Result_With_Username_erikdietrich()
        {
            const string userName = "erikdietrich";
            Inquisitor.Arrange(i => i.ExecuteUrlQuery(Arg.AnyString)).Returns(userName);

            var firstProfile = Target.GetProfilesForLocation("doesntmatter").First();

            Assert.AreEqual<string>("Dietrich", firstProfile.LastName);
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Throw_An_ArgumentNullException_On_Null_Argument()
        {
            ExtendedAssert.Throws<ArgumentNullException>(() => Target.GetProfilesForLocation(null));
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Throw_A_GithubQueryingException_When_Inquisitor_ExecuteUrlQuery_Throws_Exception()
        {
            Inquisitor.Arrange(i => i.ExecuteUrlQuery(Arg.AnyString)).Throws(new Exception());

            ExtendedAssert.Throws<GithubQueryingException>(() => Target.GetProfilesForLocation("thisdoesnotmatter"));
        }

    }
    
}
