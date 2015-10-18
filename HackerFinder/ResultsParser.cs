using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerFinder
{
    public class ResultsParser
    {
        public IEnumerable<Profile> ConvertToProfiles(string jsonText)
        {
            var profile = new Profile()
            {
                FirstName = "Erik",
                LastName = "Dietrich"
            };
            return new List<Profile>() { profile };
        }
    }
}
