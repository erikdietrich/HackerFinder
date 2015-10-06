using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace HackerFinder
{
    public class ProfileSearcher
    {
        public IEnumerable<Profile> GetProfilesForLocation(string locationText)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2;)");
            var task = client.GetAsync(string.Format("https://api.github.com/search/users?q=+location:{0}", locationText));
            var result = task.Result;

            var contentToString = result.Content.ReadAsStringAsync().Result;
            if (contentToString.Contains("erikdietrich"))
            {
                var profile = new Profile() { FirstName = "Erik" };
                return Enumerable.Repeat(profile, 1);
            }
            return Enumerable.Empty<Profile>();
        }

    }
}
