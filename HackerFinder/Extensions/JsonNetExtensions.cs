using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerFinder.Extensions
{
    public static class JsonNetExtensions
    {
        public static string KeyToString(this JToken target, string key)
        {
            return (string)target[key];
        }
    }
}
