using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerFinder.Domain
{
    public class Profile
    {
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string ProfileUrl { get; set; }

        public IList<Repository> Repos { get; set; } = new List<Repository>();

        public void AddRepository(Repository repository)
        {
            Repos.Add(repository);
        }
    }
}
