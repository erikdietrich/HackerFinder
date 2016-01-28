using HackerFinder.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerFinder.Web.Test.Controllers
{
    [TestClass]
    public class When_Retrieving_Passwords_PasswordRetriver_Should
    {
        private const string AppSettingsKey = "DummyGithubPassword";
        private const string AppSettingsValue = "DummyGithubPassword";

        private PasswordRetriever Target { get; set; }

        [TestInitialize]
        public void BeforeEachTest()
        {
            Target = new PasswordRetriever();
        }


        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Return_Empty_String_When_No_Web_Config_Key_Exists()
        {
            var password = Target.GetPassword(AppSettingsKey);

            Assert.AreEqual<string>(string.Empty, password);
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Returns_KeyValue_When_Key_Is_Not_Escaped_With_Percent()
        {
            ConfigurationManager.AppSettings[AppSettingsKey] = AppSettingsValue;

            var actualValue = Target.GetPassword(AppSettingsKey);

            Assert.AreEqual<string>(AppSettingsValue, actualValue);
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Returns_EnvironmentVariable_When_Key_Is_Escaped_With_Percent()
        {
            ConfigurationManager.AppSettings[AppSettingsKey] = "%" + AppSettingsValue;
            Environment.SetEnvironmentVariable(AppSettingsKey, AppSettingsValue, EnvironmentVariableTarget.User);

            var actualValue = Target.GetPassword(AppSettingsKey);

            Assert.AreEqual<string>(AppSettingsValue, actualValue);

            Environment.SetEnvironmentVariable(AppSettingsKey, null, EnvironmentVariableTarget.User);
        }
    }
}
