using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebFinalProject.Models;

namespace WebFinalProject.Controllers;

public class HomeController : Controller
{
    UserModel userModel = new UserModel();

    //[FetchUser]
    public IActionResult Home()
    {
        string? serializedUser = TempData["User"] as string;
        userModel = new UserModel();
        if (!string.IsNullOrEmpty(serializedUser))
        {
            userModel = JsonConvert.DeserializeObject<UserModel>(serializedUser);
        }
        return View(userModel);
    }

    //public ActionResult Home()
    //{
        //List<string> items = new List<string>
        //{
        //    "Item 1", "Item 2", "Item 3", "Item 4", "Item 5", "Item 6", "Item 5", "Item 6"  // Add more items as needed
        //};

        //return View(userModel);
    //}
}

