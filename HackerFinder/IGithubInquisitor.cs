﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerFinder
{
    public interface IGithubInquisitor
    {
        string GetLocationSearchResults(string location);

        string GetVerbatimSearchResults(string queryString);

        string GetRepoSearchResults(string repoId);

    }
}
