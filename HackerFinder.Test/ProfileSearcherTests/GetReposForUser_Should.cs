using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;

namespace HackerFinder.Test.ProfileSearcherTests
{
    [TestClass]
    public class GetReposForUser_Should
    {
        private const string NotFoundResult = @"{""message"":""NotFound"",""documentation_url"":""https://developer.github.com/v3""}";

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Should_Return_No_Profiles_For_NotFound_UserId()
        {
            var inquisitor = Mock.Create<IGithubInquisitor>();
            inquisitor.Arrange(i => i.GetRepoSearchResults(Arg.AnyString)).Returns(NotFoundResult);


            var target = new ProfileSearcher(inquisitor);

            Assert.IsFalse(target.GetReposForUser("doesntmatter").Any());
        }
    }
}
