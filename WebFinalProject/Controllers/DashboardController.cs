using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebFinalProject.Models;

namespace WebFinalProject.Controllers;

public class DashboardController : Controller
{
    public ActionResult Home()
    {
        List<string> items = new List<string>
        {
            "Item 1", "Item 2", "Item 3", "Item 4", "Item 5", "Item 6", "Item 5", "Item 6"  // Add more items as needed
        };

        return View(items);
    }

    public ActionResult Notification()
    {
        return View();
    }

    public ActionResult History()
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

