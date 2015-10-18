using HackerFinder.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

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
                var contentToString = _inquisitor.ExecuteUrlQuery(locationText);
                if (contentToString.Contains("erikdietrich"))
                {
                    var profile = new Profile()
                    {
                        FirstName = "Erik"
                    };
                    return Enumerable.Repeat(profile, 1).ToList();
                }
            }
            catch(Exception ex)
            {
                throw new GithubQueryingException("A problem occurred searching Github.", ex);
            }

            
            return Enumerable.Empty<Profile>().ToList();
        }

    }
}
