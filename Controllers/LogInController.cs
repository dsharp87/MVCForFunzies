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
                    return RedirectToAction("LandingPage","Landing");
                }
            }
            else
            {
                return View("LoginReg");
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult LogIn(LogUserViewModel formUser)
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
                    return RedirectToAction("LandingPage","Landing");
                }
                else
                {
                    TempData["error"] = "login information inccorrect. Please try again.";
                    return RedirectToAction("LoginReg");
                }
            }
        }

        [HttpPost]
        [Route("logout")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginReg");
        }

        [HttpPost]
        [Route("autoLog")]
        public IActionResult AutoLog()
        {
            HttpContext.Session.SetInt32("loggedId", 1);
            return RedirectToAction("LandingPage","Landing");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
