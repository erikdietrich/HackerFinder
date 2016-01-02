using HackerFinder.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerFinder.Test
{
    [TestClass]
    public class When_EvaluatingProfiles_ProfileEvaluator_Should
    {
        private ProfileEvaluator Target { get; set; }

        private Profile Profile { get; set; }

        private bool IsProfileAMatch { get { return Target.IsProfileAMatch(Profile); } }

        [TestInitialize]
        public void BeforeEachTest()
        {
            Target = new ProfileEvaluator();

            Profile = new Profile();
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Return_False_When_Profile_Has_No_Repositories()
        {
            Assert.IsFalse(IsProfileAMatch);
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Return_True_When_Profile_Has_A_Repository_With_A_Test()
        {
            BuildSingleRepositorySingleFileProfile("test");

            Assert.IsTrue(IsProfileAMatch);
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Return_False_When_Profile_Has_One_Repository_With_No_Files()
        {
            Profile.AddRepository(new Repository());

            Assert.IsFalse(IsProfileAMatch);
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Return_False_When_Profile_Has_A_Repository_With_Non_Test_File()
        {
            BuildSingleRepositorySingleFileProfile("no-t-word");

            Assert.IsFalse(IsProfileAMatch);
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Return_True_When_Profile_Has_A_Repository_With_Test_In_Caps()
        {
            BuildSingleRepositorySingleFileProfile("TEST");

            Assert.IsTrue(IsProfileAMatch);
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Return_True_For_File_Path_Containing_Test()
        {
            BuildSingleRepositorySingleFileProfile("blah/TeSt");

            Assert.IsTrue(IsProfileAMatch);
        }

        private void BuildSingleRepositorySingleFileProfile(string fileName)
        {
            var repository = new Repository();
            repository.AddFile(fileName);
            Profile.AddRepository(repository);
        }
    }
}
