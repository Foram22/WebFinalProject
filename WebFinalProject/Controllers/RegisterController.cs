using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebFinalProject.Controllers
{
    public class RegisterController : Controller
    {
        // GET: /<controller>/
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string name, string email, string password)
        {
            if (email == "foram@gmail.com" && password == "Foram@2211" && name == "Foram")
            {
                // Authentication successful, redirect to a dashboard or other page
                return RedirectToAction("Role", "Role");
            }
            else
            {
                // Authentication failed, show an error message
                ViewBag.ErrorMessage = "Invalid input entries";
                return View();
            }
        }
    }
}

