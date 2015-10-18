using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerFinder.Exceptions
{

    public class GithubQueryingException : Exception
    {
        public GithubQueryingException(string message, Exception ex) : base(message, ex)
        {
            
        }

    }
}
