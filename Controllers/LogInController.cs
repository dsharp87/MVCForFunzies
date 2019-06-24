using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCforFunzies.Models;

namespace MVCforFunzies.Controllers
{
    public class LogInController : Controller
    {

        private dbContext _context;

        public LogInController(dbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult LoginReg()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register()
        {
            System.Console.WriteLine("made it to post");
            return RedirectToAction("LoginReg");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
