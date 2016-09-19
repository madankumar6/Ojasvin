using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Tracker.DAL;

namespace Tracker.Web.Controllers
{
    public class HomeController : Controller
    {
        UserContext tcontext;

        public HomeController(UserContext context)
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
