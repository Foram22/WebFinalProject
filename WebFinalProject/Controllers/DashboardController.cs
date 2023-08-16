using Firebase.Database;
using Microsoft.AspNetCore.Mvc;
using WebFinalProject.Models;
using WebFinalProject.ViewModels;
using Newtonsoft.Json;

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
        string jsonStr = Request.Cookies["UserModel"];
        UserModel userModel = JsonConvert.DeserializeObject<UserModel>(jsonStr);
        return View(userModel);
    }

    public IActionResult Logout()
    {
        return RedirectToAction("Login", "Login");
    }

    public IActionResult Availability()
    {
        
        return View();
    }

    public async Task<IActionResult> FacultyAsync(string searchTerm)
    {
        var firebaseClient = new FirebaseClient("https://facultymeets-default-rtdb.firebaseio.com/");
        var faculties = await firebaseClient.Child("faculty").OnceAsync<FacultyModel>();
        var facultyData = faculties.Select(item => new FacultyModel
        {
            Id = item.Object.Id,
            Name = item.Object.Name,
        }).ToList();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            facultyData = facultyData.Where(f => f.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        var viewModel = new FacultyViewModel
        {
            Faculties = facultyData,
            SearchTerm = searchTerm
        };
        return View(viewModel);
    }
}

