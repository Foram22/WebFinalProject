using Firebase.Database;
using Microsoft.AspNetCore.Mvc;
using WebFinalProject.Models;

namespace WebFinalProject.Controllers;

public class DashboardController : Controller
{

    public IActionResult Dashboard()
    {
           
        return View();
    }

    public IActionResult Home()
    {
        
        return View();
    }

    public IActionResult Profile()
    {
        
        return View();
    }

    public IActionResult Availability()
    {
        
        return View();
    }

    public async Task<IActionResult> FacultyAsync()
    {
        var firebaseClient = new FirebaseClient("https://facultymeets-default-rtdb.firebaseio.com/");
        var faculties = await firebaseClient.Child("faculty").OnceAsync<FacultyModel>();
        var facultyData = faculties.Select(item => new FacultyModel
        {
            Id = item.Object.Id,
            Name = item.Object.Name,
        }).ToList();
        return View(facultyData);
    }
}

