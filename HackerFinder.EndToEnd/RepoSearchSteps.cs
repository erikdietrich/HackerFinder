using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using TechTalk.SpecFlow;

using static HackerFinder.Acceptance.ContextRepository;
using HackerFinder.Domain;
namespace HackerFinder.EndToEnd
{
    [Binding]
    public class RepoSearchSteps
    {
        private static IList<Repository> ReturnedRepos { get { return GetFromContext<IList<Repository>>(); } }

        [When(@"I do a repo search for user (.*)")]
        public void WhenIDoARepoSearchForUserErikdietrich(string githubUserId)
        {
            var inquisitor = new GithubInquisitor("erikdietrich", Environment.GetEnvironmentVariable("GithubPass", EnvironmentVariableTarget.User));
            var searcher = new ProfileSearcher(inquisitor);
            var repos = searcher.GetReposForUser(githubUserId);

            SetInContext(repos);
        }

        [Then(@"I should have (.*) repos")]
        public void ThenIShouldHaveRepos(int repoCount)
        {
            Assert.AreEqual<int>(repoCount, ReturnedRepos.Count());
        }
        
        [Then(@"First repo should have the following properties")]
        public void ThenFirstRepoShouldHaveTheFollowingProperties(Table table)
        {
            var firstRepo = GetFromContext<IList<Repository>>().First();
            var row = table.Rows[0];

            Assert.AreEqual<string>(row["Name"], firstRepo.Name);
            Assert.AreEqual<string>(row["Url"], firstRepo.Url);
            Assert.AreEqual<string>(row["Language"], firstRepo.Language);
            Assert.AreEqual<string>(row["Download"], firstRepo.DownloadUrl);
        }
    }
}
