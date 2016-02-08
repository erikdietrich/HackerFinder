using HackerFinder.Domain;
using HackerFinder.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HackerFinder.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProfileSearcher _searcher;

        public HomeController() : this(new PasswordRetriever())
        {

        }

        public HomeController(PasswordRetriever retriever, IProfileSearcher searcher = null)
        {
            if (searcher == null)
            {
                var githubPassword = retriever.GetPassword("GithubPass");
                var inquisitor = new GithubInquisitor("erikdietrich", githubPassword);
                _searcher = new ProfileSearcher(inquisitor);
            }
            else
                _searcher = searcher;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //Post Home/Search
        public ActionResult Search(string location, string language)
        {
            try
            {
                var profiles = _searcher.GetProfilesForLocationByTechnology(location, language);
                return View(profiles);
            }
            catch(RateLimitException)
            {
                ViewBag.ErrorMessage = "Rate limit has been exceeded.";
            }
            return View(Enumerable.Empty<Profile>());
        }
    }
}