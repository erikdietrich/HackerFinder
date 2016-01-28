using HackerFinder.Domain;
using HackerFinder.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
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
        public void Return_View_Containing_Profiles_As_Model()
        {
            var controller = new HomeController(); 

            var model = controller.Search(DefaultLocation, DefaultLanguage).GetModel<IList<Profile>>();

            Assert.IsNotNull(model);
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void User_Searcher_To_Fetch_Profiles_For_Location_By_Technology()
        {
            var searcher = Mock.Create<IProfileSearcher>();

            var controller = new HomeController(new PasswordRetriever(), searcher);

            controller.Search(DefaultLocation, DefaultLanguage);

            searcher.Assert(s => s.GetProfilesForLocationByTechnology(DefaultLocation, DefaultLanguage));
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Inovke_Retriever_When_Searcher_Is_Null()
        {
            var retriever = Mock.Create<PasswordRetriever>();

            var controller = new HomeController(retriever, null);

            retriever.Assert(r => r.GetPassword("GithubPass"), Occurs.Once());
        }
    }
}
