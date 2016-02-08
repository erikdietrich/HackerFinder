using HackerFinder.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace HackerFinder
{
    public class GithubInquisitor : IGithubInquisitor, IDisposable
    {
        public static readonly string UserAgentKey = "user-agent";
        public static readonly string UserAgentValue = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2;)";

        public HttpClient Client { get; private set; }

        public GithubInquisitor(string username = "erikdietrich", string password = null)
        { 
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Add(UserAgentKey, UserAgentValue);

            var encodedCredentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
            Client.DefaultRequestHeaders.Add(HttpRequestHeader.Authorization.ToString(), $"Basic {encodedCredentials}");
        }


        public string GetRepoSearchResults(string githubUserId)
        {
            return GetVerbatimSearchResults($"https://api.github.com/users/{githubUserId}/repos");
        }
        
        public string GetLocationSearchResults(string locationText)
        {
            return GetVerbatimSearchResults($"https://api.github.com/search/users?q=+location:{locationText}");
        }

        public string GetVerbatimSearchResults(string queryString)
        {
            var task = Client.GetAsync(queryString);
            var result = task.Result;
            var searchResults = result.Content.ReadAsStringAsync().Result;

            if (searchResults.ToLower().Contains("rate limit"))
                throw new RateLimitException();

            return searchResults;
        }

        public string GetRefForRepo(string userId, string repoId)
        {
            return GetVerbatimSearchResults($"https://api.github.com/repos/{userId}/{repoId}/git/refs/heads/master");
        }

        public string GetRecursiveTree(string userId, string repoId, string sha)
        {
            return GetVerbatimSearchResults($"https://api.github.com/repos/{userId}/{repoId}/git/trees/{sha}?recursive=1");
        }

        public void Dispose()
        {
            Client.Dispose();
        }
    }
}
