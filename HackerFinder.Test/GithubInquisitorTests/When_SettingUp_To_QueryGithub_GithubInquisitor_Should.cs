using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace HackerFinder.Test.GithubInquisitorTests
{
    [TestClass]
    public class When_SettingUp_To_QueryGithub_GithubInquisitor_Should
    {
        private GithubInquisitor Target { get; set; }

        private HttpRequestHeaders TargetHeaders { get { return Target.Client.DefaultRequestHeaders; } }

        [TestInitialize]
        public void BeforeEachTest()
        {
            Target = new GithubInquisitor();   
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Default_To_An_HttpClient_With_A_UserAgent_RequestHeader()
        {
            var requestHeaderKey = TargetHeaders.First().Key;

            Assert.AreEqual<string>(GithubInquisitor.UserAgentKey, requestHeaderKey);
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Default_To_An_HttpClient_With_UserAgent_Set_To_Mozilla()
        {
            var allRequestHeaderValues = TargetHeaders.First().Value;
            var concatenatedValuesForHeader = allRequestHeaderValues.Aggregate((i, j) => string.Format("{0} {1}", i, j));


            Assert.AreEqual<string>(GithubInquisitor.UserAgentValue, concatenatedValuesForHeader);
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Default_To_An_HttpClient_With_An_Authorization_Request_Header()
        {
            Assert.IsTrue(TargetHeaders.Any(rh => rh.Key == HttpRequestHeader.Authorization.ToString()));
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Default_To_An_HttpClient_With_An_AuthorizationRequest_Header_Matching_Username_And_Password()
        {
            const string username = "erikdietrich";
            const string password = "nottelling";
            Target = new GithubInquisitor(username, password);

            var expectedHeaderValue = $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"))}";

            var allMatchingHeaderValue = TargetHeaders.First(rh => rh.Key == HttpRequestHeader.Authorization.ToString()).Value;
            var concatenatedValuesForHeader = allMatchingHeaderValue.Aggregate((i, j) => string.Format("{0} {1}", i, j));

            Assert.AreEqual<string>(expectedHeaderValue, concatenatedValuesForHeader);
            //Assert.IsTrue(TargetHeaders.Any(rh => rh.Value == expectedHeaderValue));
        }
    }
}
