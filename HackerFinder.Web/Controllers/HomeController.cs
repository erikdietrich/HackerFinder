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

        public HomeController() : this(null)
        {

        }

        public HomeController(IProfileSearcher searcher = null)
        {
            _searcher = searcher ?? new ProfileSearcher(new GithubInquisitor("erikdietrich", Environment.GetEnvironmentVariable("GithubPass", EnvironmentVariableTarget.User)));
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
            _searcher.GetProfilesForLocationByTechnology(location, language);
            return null;
        }
    }
}