﻿namespace Portfolio.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Portfolio.Web.Models;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
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
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult DownloadResume()
        {
            return File("~/Resume/DimitarNikolovResumeFebruary2018.pdf", "application/pdf");
        }
    }
}
