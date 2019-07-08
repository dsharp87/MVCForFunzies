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
    public class ProductController: Controller
    {
        private dbContext _context;

        public ProductController(dbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Product/ViewProducts")]
        public IActionResult ViewProducts()
        {
            if(HttpContext.Session.GetInt32("loggedId") == null )
            {
                return RedirectToAction("LoginReg","LogIn");
            }
            ViewBag.AllProducts = _context.Products.OrderBy(p => p.name).ToList();
            return View();
        }

        [HttpGet]
        [Route("Product/NewProduct")]
        public IActionResult NewProduct()
        {
            if(HttpContext.Session.GetInt32("loggedId") == null )
            {
                return RedirectToAction("LoginReg","LogIn");
            }
            if(TempData["SuccessfullAddMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessfullAddMessage"];
            }
            return View();
        }

        [HttpPost]
        [Route("addProduct")]
        public IActionResult AddProduct(ProductViewModel prod)
        {
            
            if(ModelState.IsValid)
            {
                Product existsQuery = _context.Products.SingleOrDefault(p => p.name == prod.name);
                if(existsQuery != null)
                {
                    ViewBag.ExistsMessage = "A product with this name already exist, please chose a different name.";
                    return View("NewProduct");
                }
                Product newProd = new Product()
                {
                    name = prod.name,
                    description = prod.description,
                    itemUrl = prod.itemUrl,
                    price = (float)Math.Round((double)prod.price, 2)

                };
                _context.Products.Add(newProd);
                _context.SaveChanges();
                TempData["SuccessfullAddMessage"] = $"Your product, {prod.name} was successfully added!";
                return RedirectToAction("NewProduct","Product");
            }
            else
            {
                return View("NewProduct");
            }
        }
    }
}