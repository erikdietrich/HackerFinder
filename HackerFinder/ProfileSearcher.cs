using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerFinder
{
    public class ProfileSearcher
    {
        public IEnumerable<Profile> GetProfilesForLocation(string locationText)
        {
            var profile = new Profile() { FirstName = "Erik" };
            return Enumerable.Repeat(profile, 1);
        }

    }
}
