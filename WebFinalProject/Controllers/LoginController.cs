using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebFinalProject.Controllers
{
    public class LoginController : Controller
    {
        // GET: /<controller>/
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        { 
            if (username == "yourusername" && password == "yourpassword")
            {
                // Authentication successful, redirect to a dashboard or other page
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Authentication failed, show an error message
                ViewBag.ErrorMessage = "Invalid username or password";
                return View();
            }
        }
    }
}

