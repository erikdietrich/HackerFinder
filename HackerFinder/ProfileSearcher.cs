using HackerFinder.Exceptions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

using HackerFinder.Extensions;
using HackerFinder.Domain;
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
            if (locationText == null)
                throw new ArgumentNullException(nameof(locationText));

            try
            {
                return FindAllProfilesForLocation(locationText).ToList();
            }
            catch (Exception ex)
            {
                throw new GithubQueryingException("A problem occurred searching Github.", ex);
            }
        }

        public IList<Profile> GetProfilesForLocationByTechnology(string location, string language)
        {
            var profilesForLocation = GetProfilesForLocation(location);
            return profilesForLocation.Where(p => GetReposForUser(p.Username).Any(r => r.Language == language)).ToList();
        }

        public IList<Repository> GetReposForUser(string githubUserId)
        {
            if (string.IsNullOrEmpty(githubUserId))
                throw new ArgumentException(nameof(githubUserId));

            var jsonFromInquisitor = _inquisitor.GetRepoSearchResults(githubUserId);
            return BuildRepoListFromJson(jsonFromInquisitor);
        }

        private static IList<Repository> BuildRepoListFromJson(string jsonFromInquisitor)
        {
            if (JToken.Parse(jsonFromInquisitor) is JArray)
            {
                var arrayOfTokens = JArray.Parse(jsonFromInquisitor);
                return arrayOfTokens.Select(jt => MakeRepositoryFromToken(jt)).ToList();
            }
            return Enumerable.Empty<Repository>().ToList();
        }

        private IEnumerable<Profile> FindAllProfilesForLocation(string locationText)
        {
            var contentToString = _inquisitor.GetLocationSearchResults(locationText);

            var json = JObject.Parse(contentToString);
            var array = JArray.Parse(json["items"].ToString());

            for (var index = 0; index < array.Count; index++)
            {
                var profileUrl = array[index].KeyToString("url");
                var profileRawResult = _inquisitor.GetVerbatimSearchResults(profileUrl);
                var profileJson = JObject.Parse(profileRawResult);
                var profile = MakeProfileFromJson(profileJson);

                yield return profile;
            }
        }

        private Profile MakeProfileFromJson(JObject profileJson)
        {
            var nameTokens = profileJson.KeyToString("name").Split(' ');
            var profile = new Profile()
            {
                FirstName = nameTokens[0],
                LastName = nameTokens.Count() > 1 ? nameTokens[1] : string.Empty,
                EmailAddress = profileJson.KeyToString("email"),
                ProfileUrl = profileJson.KeyToString("html_url"),
                Username = profileJson.KeyToString("login")
            };

            profile.Repos = GetReposForUser(profile.Username);
            return profile;
        }

        private static Repository MakeRepositoryFromToken(JToken token)
        {
            return new Repository()
            {
                Name = token.KeyToString("name"),
                Url = token.KeyToString("html_url"),
                Language = token.KeyToString("language")
            };
        }
    }
}
