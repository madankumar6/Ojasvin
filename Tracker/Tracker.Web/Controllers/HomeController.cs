using Microsoft.AspNetCore.Mvc;

using Tracker.DAL;

namespace Tracker.Web.Controllers
{
    public class HomeController : Controller
    {
        UserDbContext tcontext;

        public HomeController(UserDbContext context)
        {
            tcontext = context;
        }

        public IActionResult Index()
        {
            //var data = tcontext.TempDatas.ToList();

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
