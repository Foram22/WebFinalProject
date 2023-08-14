using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebFinalProject.Models;

namespace WebFinalProject.Controllers;

public class ProfileController : Controller
{
    UserModel userModel = new UserModel();

    public IActionResult Profile()
    {
        return View(userModel);
    }
}

