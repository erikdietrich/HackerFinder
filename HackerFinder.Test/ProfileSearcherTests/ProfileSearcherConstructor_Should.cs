using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerFinder.Test.ProfileSearcherTests
{
    [TestClass]
    public class ProfileSearcherConstructor_Should
    {
        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Throw_ArgumentNullException_When_Passed_Null_Dependency()
        {
            ExtendedAssert.Throws<ArgumentNullException>(() => new ProfileSearcher(null));
        }
    }
}
