using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace HackerFinder
{
    public class GithubInquisitor : IGithubInquisitor, IDisposable
    {
        public static readonly string UserAgentKey = "user-agent";
        public static readonly string UserAgentValue = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2;)";

        public HttpClient Client { get; } = new HttpClient();

        public string GetRepoSearchResults(string repoId)
        {
            throw new NotImplementedException();
        }
        public GithubInquisitor()
        {
            Client.DefaultRequestHeaders.Add(UserAgentKey, UserAgentValue);
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
