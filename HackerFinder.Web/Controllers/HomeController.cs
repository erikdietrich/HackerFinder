using CsvHelper;
using HackerFinder.Domain;
using HackerFinder.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
namespace HackerFinder.Web.Controllers
{
    public class HomeController : Controller
    {
        public const string Filename = "results.csv";
        public const string FileType = "text/csv";
        public const string RateLimitErrorMessage = "Rate limit has been exceeded.";

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
                ViewBag.ErrorMessage = RateLimitErrorMessage;
            }
            return View(Enumerable.Empty<Profile>());
        }

        //Post Home/Export
        public FileContentResult Export(string location, string language)
        {
            try
            {
                return BuildFileContentResult(location, language);
            }
            catch (RateLimitException)
            {
                return File(new UTF8Encoding().GetBytes(RateLimitErrorMessage), FileType, Filename);
            }

        }
        private FileContentResult BuildFileContentResult(string location, string language)
        {
            var profiles = _searcher.GetProfilesForLocationByTechnology(location, language);
            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream))
                using (var csvWriter = new CsvWriter(streamWriter))
                {
                    csvWriter.WriteRecords(profiles);
                }
                return File(memoryStream.ToArray(), FileType, Filename);
            }
        }
    }
}