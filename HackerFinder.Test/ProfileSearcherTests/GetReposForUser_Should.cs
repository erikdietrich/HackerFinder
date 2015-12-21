using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;

namespace HackerFinder.Test.ProfileSearcherTests
{
    [TestClass]
    public class GetReposForUser_Should
    {
        private const string NotFoundResult = @"{""message"":""NotFound"",""documentation_url"":""https://developer.github.com/v3""}";
        private const string SingleResult = @"[{""id"":8161886,""name"":""ASPNETWebAPISamples"",""full_name"":""erikdietrich/ASPNETWebAPISamples"",""owner"":{""login"":""erikdietrich"",""id"":1232840,""avatar_url"":""https://avatars.githubusercontent.com/u/1232840?v=3"",""gravatar_id"":"""",""url"":""https://api.github.com/users/erikdietrich"",""html_url"":""https://github.com/erikdietrich"",""followers_url"":""https://api.github.com/users/erikdietrich/followers"",""following_url"":""https://api.github.com/users/erikdietrich/following{/other_user}"",""gists_url"":""https://api.github.com/users/erikdietrich/gists{/gist_id}"",""starred_url"":""https://api.github.com/users/erikdietrich/starred{/owner}{/repo}"",""subscriptions_url"":""https://api.github.com/users/erikdietrich/subscriptions"",""organizations_url"":""https://api.github.com/users/erikdietrich/orgs"",""repos_url"":""https://api.github.com/users/erikdietrich/repos"",""events_url"":""https://api.github.com/users/erikdietrich/events{/privacy}"",""received_events_url"":""https://api.github.com/users/erikdietrich/received_events"",""type"":""User"",""site_admin"":false},""private"":false,""html_url"":""https://github.com/erikdietrich/ASPNETWebAPISamples"",""description"":"""",""fork"":true,""url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples"",""forks_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/forks"",""keys_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/keys{/key_id}"",""collaborators_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/collaborators{/collaborator}"",""teams_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/teams"",""hooks_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/hooks"",""issue_events_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/issues/events{/number}"",""events_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/events"",""assignees_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/assignees{/user}"",""branches_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/branches{/branch}"",""tags_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/tags"",""blobs_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/git/blobs{/sha}"",""git_tags_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/git/tags{/sha}"",""git_refs_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/git/refs{/sha}"",""trees_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/git/trees{/sha}"",""statuses_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/statuses/{sha}"",""languages_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/languages"",""stargazers_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/stargazers"",""contributors_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/contributors"",""subscribers_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/subscribers"",""subscription_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/subscription"",""commits_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/commits{/sha}"",""git_commits_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/git/commits{/sha}"",""comments_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/comments{/number}"",""issue_comment_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/issues/comments{/number}"",""contents_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/contents/{+path}"",""compare_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/compare/{base}...{head}"",""merges_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/merges"",""archive_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/{archive_format}{/ref}"",""downloads_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/downloads"",""issues_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/issues{/number}"",""pulls_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/pulls{/number}"",""milestones_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/milestones{/number}"",""notifications_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/notifications{?since,all,participating}"",""labels_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/labels{/name}"",""releases_url"":""https://api.github.com/repos/erikdietrich/ASPNETWebAPISamples/releases{/id}"",""created_at"":""2013-02-12T16:01:49Z"",""updated_at"":""2013-02-12T16:01:49Z"",""pushed_at"":""2013-02-07T00:10:38Z"",""git_url"":""git://github.com/erikdietrich/ASPNETWebAPISamples.git"",""ssh_url"":""git@github.com:erikdietrich/ASPNETWebAPISamples.git"",""clone_url"":""https://github.com/erikdietrich/ASPNETWebAPISamples.git"",""svn_url"":""https://github.com/erikdietrich/ASPNETWebAPISamples"",""homepage"":"""",""size"":2346,""stargazers_count"":0,""watchers_count"":0,""language"":""JavaScript"",""has_issues"":false,""has_downloads"":true,""has_wiki"":true,""has_pages"":false,""forks_count"":0,""mirror_url"":null,""open_issues_count"":0,""forks"":0,""open_issues"":0,""watchers"":0,""default_branch"":""master""}]";

        private IGithubInquisitor Inquisitor { get; set; }

        private ProfileSearcher Target { get; set; }

        [TestInitialize]
        public void BeforeEachTest()
        {
            Inquisitor = Mock.Create<IGithubInquisitor>();
            Inquisitor.Arrange(i => i.GetRepoSearchResults(Arg.AnyString)).Returns(SingleResult);

            Target = new ProfileSearcher(Inquisitor);
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Should_Return_No_Repositories_For_NotFound_UserId()
        {
            Inquisitor.Arrange(i => i.GetRepoSearchResults(Arg.AnyString)).Returns(NotFoundResult);

            var repos = Target.GetReposForUser("doesntmatter");

            Assert.IsFalse(repos.Any());
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Should_Return_SingleRepository_For_Single_Result()
        {
            var repositoryCount = Target.GetReposForUser("whatever").Count();

            Assert.AreEqual<int>(1, repositoryCount);
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Should_Return_A_Repository_With_Name_Matching_SingleResult_Name()
        {
            var firstRepo = Target.GetReposForUser("dontmatter").First();

            Assert.AreEqual<string>("ASPNETWebAPISamples", firstRepo.Name);
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Should_Return_A_Repository_With_Url_Matching_SingleResult_Html_Url()
        {
            var firstRepo = Target.GetReposForUser("whatever").First();

            Assert.AreEqual<string>("https://github.com/erikdietrich/ASPNETWebAPISamples", firstRepo.Url);
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Should_Return_A_Repository_With_Language_Matching_SingleResult_Language()
        {
            var firstRepo = Target.GetReposForUser("stilldoesn'tmatter").First();

            Assert.AreEqual<string>("JavaScript", firstRepo.Language);
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Throw_Exception_When_githubUserId_Is_Null()
        {
            ExtendedAssert.Throws<ArgumentException>(() => Target.GetReposForUser(null));
        }
    }
}
