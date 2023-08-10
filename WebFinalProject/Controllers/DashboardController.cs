using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebFinalProject.Models;

namespace WebFinalProject.Controllers;

public class DashboardController : Controller
{
    public ActionResult Home()
    {
        return View();
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

