using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebFinalProject.Models;

namespace WebFinalProject.Controllers;

public class FacultyController : Controller
{
    UserModel userModel = new UserModel();

    public IActionResult Faculty()
    {
        return View(userModel);
    }
}

