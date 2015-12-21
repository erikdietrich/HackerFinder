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
            var task = Client.GetAsync($"https://api.github.com/users/{githubUserId}/repos");
            var result = task.Result;
            return result.Content.ReadAsStringAsync().Result;
        }
        
        public string GetLocationSearchResults(string locationText)
        {
            var task = Client.GetAsync(string.Format("https://api.github.com/search/users?q=+location:{0}", locationText));
            var result = task.Result;
            return result.Content.ReadAsStringAsync().Result;
        }

        public string GetVerbatimSearchResults(string queryString)
        {
            var task = Client.GetAsync(queryString);
            var result = task.Result;
            return result.Content.ReadAsStringAsync().Result;
        }

        public void Dispose()
        {
            Client.Dispose();
        }

        
    }
}
