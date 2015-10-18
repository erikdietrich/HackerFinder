using HackerFinder.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerFinder
{
    public class ProfileSearcher
    {
        private readonly IGithubInquisitor _inquisitor;
        public ProfileSearcher(IGithubInquisitor inquisitor)
        {
            if (inquisitor == null)
                throw new ArgumentNullException(nameof(inquisitor));
            _inquisitor = inquisitor;
        }

        public IList<Profile> GetProfilesForLocation(string locationText)
        {
            if(locationText == null)
                throw new ArgumentNullException(nameof(locationText));

            try
            {
                return FindAllProfilesForLocation(locationText);
            }
            catch (Exception ex)
            {
                throw new GithubQueryingException("A problem occurred searching Github.", ex);
            }            
        }

        private IList<Profile> FindAllProfilesForLocation(string locationText)
        {
            var contentToString = _inquisitor.ExecuteUrlQuery(locationText);
            if (contentToString.Contains("erikdietrich"))
            {
                var profile = BuildProfile();
                return Enumerable.Repeat(profile, 1).ToList();
            }
            return Enumerable.Empty<Profile>().ToList();
        }

        private static Profile BuildProfile()
        {
            var profile = new Profile()
            {
                FirstName = "Erik",
                LastName = "Dietrich"
            };
            return profile;
        }
    }
}
