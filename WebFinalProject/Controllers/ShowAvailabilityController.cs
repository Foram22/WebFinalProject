using Firebase.Database;
using Microsoft.AspNetCore.Mvc;
using WebFinalProject.Models;
using WebFinalProject.ViewModels;
using Newtonsoft.Json;
using Firebase.Database.Query;

namespace WebFinalProject.Controllers;

public class ShowAvailabilityController : Controller
{
    string facultyId = "";
    List<AvailabilityModel>? availabilityData;
    UserModel userModel = new UserModel();


    public async Task<IActionResult> ShowAvailabilityAsync(string id)
    {
        facultyId = id;

        string jsonStr = Request.Cookies["UserModel"];
        userModel = JsonConvert.DeserializeObject<UserModel>(jsonStr);

        var firebaseClient = new FirebaseClient("https://facultymeets-default-rtdb.firebaseio.com/");
        var availabilities = await firebaseClient.Child("faculty").Child(facultyId).Child("availabilities").OnceAsync<AvailabilityModel>();
        availabilityData = availabilities.Select(item => new AvailabilityModel
        {
            Id = item.Key,
            FacultyId = facultyId,
            StartTime = item.Object.StartTime,
            EndTime = item.Object.EndTime
        }).ToList();

        if (userModel.Role == "faculty" || userModel.Role == "Faculty") {
            ViewBag.IsFaculty = true;
        }
        else
        {
            ViewBag.IsFaculty = false;
        }
        
        return View(availabilityData);
    }

    [HttpPost]
    public ActionResult Appointment(string modelData)
    {
        AvailabilityModel model = JsonConvert.DeserializeObject<AvailabilityModel>(modelData);
        return View(model);
    }


    public async Task<ActionResult> BookAppointment(string facultyId, DateTime startTime, DateTime endTime, string id)
    {
        string jsonStr = Request.Cookies["UserModel"];
        userModel = JsonConvert.DeserializeObject<UserModel>(jsonStr);

        AppointmentModel appointmentModel = new AppointmentModel();
        appointmentModel.FacultyId = facultyId;
        appointmentModel.AppointmentEndTime = endTime;
        appointmentModel.AppointmentStartTime = startTime;
        appointmentModel.StudentId = userModel.Id;

        var firebaseClient = new FirebaseClient("https://facultymeets-default-rtdb.firebaseio.com/");

        await firebaseClient.Child("users").Child(userModel.Id).Child("appointments").PostAsync(appointmentModel);
        await firebaseClient.Child("users").Child(facultyId).Child("appointments").PostAsync(appointmentModel);
        await firebaseClient.Child("faculty").Child(facultyId).Child("appointments").PostAsync(appointmentModel);

        ViewBag.ToastMessage = "Appointment booked successfully.";

        return RedirectToAction("ShowAvailability", "ShowAvailability", new { id = facultyId});
    }

}

