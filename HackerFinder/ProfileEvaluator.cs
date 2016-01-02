using HackerFinder.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerFinder
{
    public class ProfileEvaluator
    {
        public bool IsProfileAMatch(Profile profile)
        {
            return profile.Repos.Any(r => IsRepositoryAMatch(r));
        }

        private static bool IsRepositoryAMatch(Repository repositoryToEvaluate)
        {
            return repositoryToEvaluate.Files.Any(file => IsATestFile(file));
        }
        private static bool IsATestFile(string file)
        {
            return file.ToLower().Contains("test");
        }
    }
}
