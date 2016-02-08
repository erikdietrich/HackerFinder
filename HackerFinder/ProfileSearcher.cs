using HackerFinder.Exceptions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

using HackerFinder.Extensions;
using HackerFinder.Domain;
namespace HackerFinder
{
    public class ProfileSearcher : IProfileSearcher
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
            return BuildRepoListFromJson(githubUserId, jsonFromInquisitor);
        }

        private IList<Repository> BuildRepoListFromJson(string githubUserId, string jsonFromInquisitor)
        {
            if (JToken.Parse(jsonFromInquisitor) is JArray)
            {
                var arrayOfTokens = JArray.Parse(jsonFromInquisitor);
                return arrayOfTokens.Select(jt => MakeRepositoryFromToken(githubUserId, jt)).ToList();
            }
            return Enumerable.Empty<Repository>().ToList();
        }

        private IEnumerable<Profile> FindAllProfilesForLocation(string locationText)
        {
            var contentToString = _inquisitor.GetLocationSearchResults(locationText);

            var json = JObject.Parse(contentToString);
            var token = json["items"];
            if (token != null)
            {
                var array = JArray.Parse(token.ToString());

                for (var index = 0; index < array.Count; index++)
                {
                    var profileUrl = array[index].KeyToString("url");
                    var profileRawResult = _inquisitor.GetVerbatimSearchResults(profileUrl);
                    var profileJson = JObject.Parse(profileRawResult);
                    var profile = MakeProfileFromJson(profileJson);

                    yield return profile;
                }
            }
        }

        #region ExtractToProfileMaker

        private Profile MakeProfileFromJson(JObject profileJson)
        {
            var profile = new Profile()
            {
                FirstName = GetFirstNameFromProfileJson(profileJson),
                LastName = GetLastNameFromProfileJson(profileJson),
                EmailAddress = profileJson.KeyToString("email"),
                ProfileUrl = profileJson.KeyToString("html_url"),
                Username = profileJson.KeyToString("login")
            };

            profile.Repos = GetReposForUser(profile.Username);
            return profile;
        }

        private static string GetLastNameFromProfileJson(JObject profileJson)
        {
            var nameTokens = TokenizeName(profileJson);
            return nameTokens?.Count() > 1 ? nameTokens[1] : string.Empty;
        }

        private static string GetFirstNameFromProfileJson(JObject profileJson)
        {
            return TokenizeName(profileJson)?[0];
        }

        private static string[] TokenizeName(JObject profileJson)
        {
            return profileJson.KeyToString("name")?.Split(' ');
        }

        #endregion

        private Repository MakeRepositoryFromToken(string githubUserId, JToken token)
        {
            var repo = new Repository()
            {
                Name = token.KeyToString("name"),
                Url = token.KeyToString("html_url"),
                Language = token.KeyToString("language")
            };

            AddShaToRepo(githubUserId, repo);
            AddFileTreeToRepo(githubUserId, repo);

            return repo;
        }
        private void AddShaToRepo(string githubUserId, Repository repo)
        {
            var refJsonFromInquisitor = _inquisitor.GetRefForRepo(githubUserId, repo.Name);
            if (!string.IsNullOrEmpty(refJsonFromInquisitor))
            {
                var refJson = JObject.Parse(refJsonFromInquisitor);
                if (refJson.Property("object") != null)
                    repo.Sha = refJson["object"]["sha"].ToString();
            }
        }
        private void AddFileTreeToRepo(string githubUserId, Repository repo)
        {
            var treeJsonFromInquisitor = _inquisitor.GetRecursiveTree(githubUserId, repo.Name, repo.Sha);
            if (!string.IsNullOrEmpty(treeJsonFromInquisitor))
            {
                var treeJson = JObject.Parse(treeJsonFromInquisitor);
                if (treeJson.Property("tree") != null)
                {
                    var jsonArray = JArray.Parse(treeJson["tree"].ToString());
                    for (var index = 0; index < jsonArray.Count; index++)
                        repo.Files.Add(jsonArray[index].KeyToString("path"));
                }
            }
        }
    }
}
