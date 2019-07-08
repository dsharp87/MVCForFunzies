using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCforFunzies.Models;

namespace MVCforFunzies
{
    public class LandingController: Controller
    {
        private dbContext _context;

        public LandingController(dbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("landing")]
        public IActionResult LandingPage()
        {
            if(HttpContext.Session.GetInt32("loggedId") == null )
            {
                return RedirectToAction("LoginReg","LogIn");
            }
            int loggedId = (int)HttpContext.Session.GetInt32("loggedId");
            User loggedUser = _context.Users.SingleOrDefault(u => u.UserId == loggedId);
            System.Console.WriteLine(loggedUser.firstName);
            ViewBag.loggedUser = loggedUser;
            return View();
        }

    }
}