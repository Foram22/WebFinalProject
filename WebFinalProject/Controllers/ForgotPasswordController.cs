using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.AspNetCore.Mvc;
using WebFinalProject.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebFinalProject.Controllers
{
    public class ForgotPasswordController : Controller
    {
        // GET: /<controller>/
        public ActionResult Forgot()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ForgotAsync(string email, string password, string confirm_password)
        {
            if (password == confirm_password)
            {
                try
                {
                    var firebaseClient = new FirebaseClient("https://facultymeets-default-rtdb.firebaseio.com/");
                    var userRef = await firebaseClient.Child("users").OrderBy("Email").EqualTo(email).OnceAsync<UserModel>();

                    if (userRef.Any())
                    {
                        var user = userRef.First().Object;
                        user.Password = password;

                        if (user.Role == "Faculty" || user.Role == "faculty")
                        {
                            await firebaseClient.Child("faculty").Child(user.Id).PutAsync(user);
                        }
                        await firebaseClient.Child("users").Child(user.Id).PutAsync(user);

                        return RedirectToAction("Login", "Login");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Email not found.";
                        return View("Forgot");
                    }
                }
                catch (FirebaseAuthException ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                    ViewBag.ErrorMessage = ex.Message;
                    return View("Forgot");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Password mismatch";
                return View("Forgot");
            }

            //if (email != null && password != null && password == confirm_password)
            //{
            //    return RedirectToAction("Login", "Login");
            //}
            //else
            //{
            //    // Authentication failed, show an error message
            //    ViewBag.ErrorMessage = "Invalid username or password";
            //    return View();
            //}
        }
    }
}

