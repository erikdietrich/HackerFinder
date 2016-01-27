using System;
using System.Collections.Generic;
using System.Linq;
using HackerFinder.Domain;

namespace HackerFinder
{
    public interface IProfileSearcher
    {
        IList<Profile> GetProfilesForLocation(string locationText);
        IList<Profile> GetProfilesForLocationByTechnology(string location, string language);
        IList<Repository> GetReposForUser(string githubUserId);
    }
}
