using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerFinder.Test.GithubInquisitorTests
{
    [TestClass]
    public class When_SettingUp_To_QueryGithub_GithubInquisitor_Should
    {
        private GithubInquisitor Target { get; set; }

        [TestInitialize]
        public void BeforeEachTest()
        {
            Target = new GithubInquisitor();   
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Default_To_An_HttpClient_With_A_UserAgent_RequestHeader()
        {
            var requestHeaderKey = Target.Client.DefaultRequestHeaders.First().Key;

            Assert.AreEqual<string>(GithubInquisitor.UserAgentKey, requestHeaderKey);
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Default_To_An_HttpClient_With_UserAgent_Set_To_Mozilla()
        {
            var allRequestHeaderValues = Target.Client.DefaultRequestHeaders.First().Value;
            var concatenatedValuesForHeader = allRequestHeaderValues.Aggregate((i, j) => string.Format("{0} {1}", i, j));


            Assert.AreEqual<string>(GithubInquisitor.UserAgentValue, concatenatedValuesForHeader);
        }
    }
}
