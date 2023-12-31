﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebFinalProject.Controllers
{
    public class RoleController : Controller
    {
        // GET: /<controller>/
        public ActionResult Role()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Role(string role)
        {
            if (role != null)
            {
                return RedirectToAction("Login", "Login");
            }
            else {
                ViewBag.SelectedRole = "Please select any role.";
                return View();
            }
        }
    }
}

