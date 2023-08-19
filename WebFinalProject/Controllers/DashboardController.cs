using Firebase.Database;
using Microsoft.AspNetCore.Mvc;
using WebFinalProject.Models;
using WebFinalProject.ViewModels;
using Newtonsoft.Json;
using Firebase.Database.Query;
using System.Globalization;

namespace WebFinalProject.Controllers;

public class DashboardController : Controller
{

    public IActionResult Dashboard()
    {

        return View();
    }

    public async Task<IActionResult> HomeAsync()
    {
        string jsonStr = Request.Cookies["UserModel"];
        UserModel userModel = JsonConvert.DeserializeObject<UserModel>(jsonStr);

        var firebaseClient = new FirebaseClient("https://facultymeets-default-rtdb.firebaseio.com/");
        var appointments = await firebaseClient.Child("users").Child(userModel.Id).Child("appointments").OnceAsync<AppointmentModel>();
        var appointmentList = new List<AppointmentModel>();

        foreach (var item in appointments)
        {
            var name = await GetNameFromDB(item.Object, userModel);
            var appointment = new AppointmentModel
            {
                AppointmentEndTime = item.Object.AppointmentEndTime,
                AppointmentStartTime = item.Object.AppointmentStartTime,
                FacultyId = item.Object.FacultyId,
                StudentId = item.Object.StudentId,
                Name = name
            };

            appointmentList.Add(appointment);
        }

        return View(appointmentList);
    }

    private async Task<string> GetNameFromDB(AppointmentModel appointment, UserModel userModel)
    {
        UserModel user;
        var firebaseClient = new FirebaseClient("https://facultymeets-default-rtdb.firebaseio.com/");

        if (userModel.Role == "Faculty" || userModel.Role == "faculty")
        {
            var users = await firebaseClient.Child("users").Child(appointment.StudentId).OnceSingleAsync<UserModel>();
            user = new UserModel
            {
                Name = users.Name,
                Email = users.Email,
                Password = users.Password,
                Id = users.Id,
                Role = users.Role
            };
        }
        else
        {
            var users = await firebaseClient.Child("users").Child(appointment.FacultyId).OnceSingleAsync<UserModel>();
            user = new UserModel
            {
                Name = users.Name,
                Email = users.Email,
                Password = users.Password,
                Id = users.Id,
                Role = users.Role
            };
        }


        return user.Name;
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

    [HttpPost]
    public async Task<IActionResult> Availability(AvailabilityModel model)
    {
        string jsonStr = Request.Cookies["UserModel"];
        UserModel userModel = JsonConvert.DeserializeObject<UserModel>(jsonStr);

        var firebaseClient = new FirebaseClient("https://facultymeets-default-rtdb.firebaseio.com/");

        model.FacultyId = userModel.Id;

        await firebaseClient.Child("users").Child(userModel.Id).Child("availabilities").PostAsync(model);
        await firebaseClient.Child("faculty").Child(userModel.Id).Child("availabilities").PostAsync(model);
        return RedirectToAction("ShowAvailability","ShowAvailability", new { id = userModel.Id});
    }

    // GET: Show the availability form
    [HttpGet]
    public IActionResult Availability()
    {
        return View(new AvailabilityModel());
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

