using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerFinder
{
    public interface IGithubInquisitor
    {
        string GetLocationSearchResults(string location);

        string GetVerbatimSearchResults(string queryString);

        string GetRepoSearchResults(string repoId);

        string GetRefForRepo(string userId, string repoId);

        string GetRecursiveTree(string userId, string repoId, string sha);

    }
}
