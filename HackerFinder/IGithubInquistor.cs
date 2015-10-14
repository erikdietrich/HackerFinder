using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace HackerFinder
{
    public interface IGithubInquistor
    {
        string ExecuteUrlQuery(string queryString);

    }
}
