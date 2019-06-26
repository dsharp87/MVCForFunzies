using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
            if (TempData["error"] != null) 
            {
                ViewBag.Error = TempData["error"];
            }
            return View();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(UserViewModel formUser)
        {
            if(ModelState.IsValid)
            {
                User newUser = new User {
                    firstName = formUser.firstName,
                    lastName = formUser.lastName,
                    email = formUser.email,
                    password = formUser.password
                };
                User emailExistsQuery = _context.Users.SingleOrDefault(user => user.email == formUser.email);
                if(emailExistsQuery != null)
                {
                    ViewBag.ExistsError = "User with this email already exists, please choose a different email";
                    return View("LoginReg");
                }
                else
                {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    newUser.password = Hasher.HashPassword(newUser, newUser.password);
                    _context.Add(newUser);
                    _context.SaveChanges();
                    User results = _context.Users.SingleOrDefault(user => user.email == formUser.email);
                    HttpContext.Session.SetInt32("loggedId", results.UserId);
                    return RedirectToAction("OtherPage");
                }
            }
            else
            {
                return View("LoginReg");
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LogUserViewModel formUser)
        {
            User results = _context.Users.SingleOrDefault(u => u.email == formUser.email);
            if(results == null)
            {
                TempData["error"] = "login information inccorrect. Please try again.";
                return RedirectToAction("LoginReg");
            }
            else
            {
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                if(Hasher.VerifyHashedPassword(results, results.password, formUser.password) != 0) 
                {
                    HttpContext.Session.SetInt32("loggedId", results.UserId);
                    return RedirectToAction("OtherPage");
                }
            }
            return RedirectToAction("OtherPage");
        }


        [HttpGet]
        [Route("other")]
        public IActionResult OtherPage()
        {
            if(HttpContext.Session.GetInt32("loggedId") == null )
            {
                return RedirectToAction("LoginReg");
            }
            int loggedId = (int)HttpContext.Session.GetInt32("loggedId");
            User loggedUser = _context.Users.SingleOrDefault(u => u.UserId == loggedId);
            System.Console.WriteLine(loggedUser.firstName);
            ViewBag.loggedUser = loggedUser;
            return View();
        }


        [HttpPost]
        [Route("logout")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginReg");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
