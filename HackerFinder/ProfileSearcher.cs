using HackerFinder.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerFinder
{
    public class ProfileSearcher
    {
        private readonly IGithubInquisitor _inquisitor;

        private readonly ResultsParser _parser = new ResultsParser();

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
                var profiles = _parser.ConvertToProfiles(contentToString);
                return profiles.ToList();
            }
            return Enumerable.Empty<Profile>().ToList();
        }
    }
}
