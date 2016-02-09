using HackerFinder.Domain;
using HackerFinder.Exceptions;
using HackerFinder.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;


namespace HackerFinder.Web.Test.Controllers.HomeControllerTests
{
    [TestClass]
    public class When_Performing_A_Candidate_CSV_Export_HomeController_Should
    {
        private const string DefaultLocation = "somelocation";
        private const string DefaultLanguage = "somelanguage";

        private IProfileSearcher Searcher { get; set; }
        private HomeController Target { get; set; }
        private Profile DefaultProfile { get; set; }

        [TestInitialize]
        public void BeforeEachTest()
        {
            DefaultProfile = new Profile()
            {
                FirstName = "Erik",
                LastName = "Dietrich"
            };
            Searcher = Mock.Create<IProfileSearcher>();

            var profiles = new List<Profile>() { DefaultProfile };
            Searcher.Arrange(searcher => searcher.GetProfilesForLocationByTechnology(DefaultLocation, DefaultLanguage)).Returns(profiles);

            Target = new HomeController(new PasswordRetriever(), Searcher);
        }


        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Return_A_FileResult_With_Download_Name_results_dot_csv()
        {
            var result = Target.Export(DefaultLocation, DefaultLanguage);

            Assert.AreEqual<string>(HomeController.Filename, result.FileDownloadName);

        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Return_A_FileResult_With_Type_text_slash_csv()
        {
            var result = Target.Export(DefaultLocation, DefaultLanguage);

            Assert.AreEqual<string>(HomeController.FileType, result.ContentType);
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Return_A_File_Result_Containing_Profile_FirstName()
        {
            var fileContents = GetContentsAsString(Target.Export(DefaultLocation, DefaultLanguage));

            Assert.IsTrue(fileContents.Contains(DefaultProfile.FirstName));
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Return_A_File_Result_Containing_Profile_LastName()
        {
            var fileContents = GetContentsAsString(Target.Export(DefaultLocation, DefaultLanguage));

            Assert.IsTrue(fileContents.Contains(DefaultProfile.LastName));
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Return_A_File_With_RateLimitExceeded_Message_If_Rate_Limit_Is_Exceeded()
        {
            Searcher.Arrange(searcher => searcher.GetProfilesForLocationByTechnology(Arg.AnyString, Arg.AnyString)).Throws(new RateLimitException());

            var fileContents = GetContentsAsString(Target.Export(DefaultLocation, DefaultLanguage));

            Assert.IsTrue(fileContents.Contains(HomeController.RateLimitErrorMessage));
        }

        private static string GetContentsAsString(FileContentResult result)
        {
            return Encoding.Default.GetString(result.FileContents);
        }
    }
}
