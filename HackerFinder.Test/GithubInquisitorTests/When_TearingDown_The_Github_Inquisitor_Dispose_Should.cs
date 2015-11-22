using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerFinder.Test.GithubInquisitorTests
{
    [TestClass]
    public class When_TearingDown_The_Github_Inquisitor_Dispose_Should
    {
        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Dispose_Of_The_Http_Client()
        {
            var target = new GithubInquisitor();

            target.Dispose();

            ExtendedAssert.Throws(() => target.Client.CancelPendingRequests());
        }
    }
}
