using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace HackerFinder.Web.Controllers
{
    public class PasswordRetriever
    {
        public virtual string GetPassword(string githubPasswordKey)
        {
            var valueFromConfigFile = ConfigurationManager.AppSettings[githubPasswordKey];

            if (string.IsNullOrEmpty(valueFromConfigFile))
                return string.Empty;
            else if (valueFromConfigFile[0] == '%')
                return Environment.GetEnvironmentVariable(githubPasswordKey, EnvironmentVariableTarget.User);
            else
                return valueFromConfigFile;

        }
    }
}