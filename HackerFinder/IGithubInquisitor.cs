using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerFinder
{
    public interface IGithubInquisitor
    {
        string ExecuteLocationSearch(string queryString);
        string ExecuteVerbatimSearch(string queryString);

    }
}
