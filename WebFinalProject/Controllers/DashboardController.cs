using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebFinalProject.Models;

namespace WebFinalProject.Controllers;

public class DashboardController : Controller
{
    public ActionResult Home()
    {
        //string? serializedUser = TempData["User"] as string;
        //UserModel userModel = new UserModel();
        //var isFaculty = false;
        //if (!string.IsNullOrEmpty(serializedUser))
        //{
        //    userModel = JsonConvert.DeserializeObject<UserModel>(serializedUser);

        //    if (userModel != null && userModel.Role.Equals("Faculty")) {
        //        isFaculty = true;
        //    }
        //}

        //var viewModel = userModel;

        List<string> items = new List<string>
        {
            "Item 1", "Item 2", "Item 3", "Item 4", "Item 5", "Item 6", "Item 5", "Item 6"  // Add more items as needed
        };

        return View(items);
    }

    public ActionResult Faculty()
    {
        return View();
    }

    public ActionResult Setting()
    {
        return View();
    }

    public ActionResult Profile()
    {
        return View();
    }
}

