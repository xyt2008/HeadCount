using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HeadCount.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "人数统计";
            ViewData["Num"] = Startup.Num;
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
