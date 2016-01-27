using HackerFinder.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;

namespace HackerFinder.Web.Test.Controllers.HomeControllerTests
{
    [TestClass]
    public class When_Performing_A_Candidate_Search_HomeController_Should
    {
        private const string DefaultLocation = "somelocation";

        private const string DefaultLanguage = "somelanguage";

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Return_Null()
        {
            var controller = new HomeController();

            Assert.IsNull(controller.Search(DefaultLocation, DefaultLanguage));
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void User_Searcher_To_Fetch_Profiles_For_Location_By_Technology()
        {
            var searcher = Mock.Create<IProfileSearcher>();

            var controller = new HomeController(searcher);

            controller.Search(DefaultLocation, DefaultLanguage);

            searcher.Assert(s => s.GetProfilesForLocationByTechnology(DefaultLocation, DefaultLanguage));
        }
    }
}
