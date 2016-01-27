using HackerFinder.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerFinder.Web.Test.Controllers.HomeControllerTests
{
    [TestClass]
    public class When_Performing_A_Candidate_Search_HomeController_Should
    {
        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Return_Null()
        {
            var controller = new HomeController();

            Assert.IsNull(controller.Search("somelocation", "somelanguage"));
        }
    }
}
