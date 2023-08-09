using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebFinalProject.Controllers
{
    public class ForgotPasswordController : Controller
    {
        // GET: /<controller>/
        public ActionResult Forgot()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Forgot(string email, string password, string confirm_password)
        {
            if (email != null && password != null && password == confirm_password)
            {
                return RedirectToAction("Login", "Login");
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

