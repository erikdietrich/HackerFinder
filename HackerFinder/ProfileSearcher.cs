using HackerFinder.Exceptions;
using Newtonsoft.Json.Linq;
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
            var contentToString = _inquisitor.ExecuteLocationSearch(locationText);
            var json = JObject.Parse(contentToString);
            var profileUrl = (string)json["items"][0]["url"];

            var profileRawResult = _inquisitor.ExecuteVerbatimSearch(profileUrl);
            var profileJson = JObject.Parse(profileRawResult);
            var profile = MakeProfileFromJson(profileJson);

            return new List<Profile>() { profile };
        }
        private static Profile MakeProfileFromJson(JObject profileJson)
        {
            var nameTokens = ((string)profileJson["name"]).Split(' ');
            var profile = new Profile()
            {
                FirstName = nameTokens[0],
                LastName = nameTokens[1],
                EmailAddress = (string)profileJson["email"]
            };
            return profile;
        }
    }
}
