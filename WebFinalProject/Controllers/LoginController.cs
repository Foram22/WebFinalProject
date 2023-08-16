using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebFinalProject.Models;
using WebFinalProject.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebFinalProject.Controllers
{
    public class LoginController : Controller
    {
        FirebaseAuthProvider auth;
        public LoginController()
        {
            auth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyCGXTsFk3C8W5k8pA2rLmbv6jag8CJ95_8"));
        }


        // GET: /<controller>/
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                //create the user
                var authResult = await auth.SignInWithEmailAndPasswordAsync(email, password);

                if (authResult != null && authResult.User != null)
                {
                    var userId = authResult.User.LocalId;

                    var firebaseClient = new FirebaseClient("https://facultymeets-default-rtdb.firebaseio.com/");
                    var users = await firebaseClient.Child("users").Child(userId).OnceSingleAsync<UserModel>();
                    UserModel userModel = new UserModel
                    {
                        Name = users.Name,
                        Email = users.Email,
                        Password = users.Password,
                        Id = users.Id,
                        Role = users.Role
                    };

                    TempData["User"] = JsonConvert.SerializeObject(userModel);

                    var cookieOptions = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(1),
                        HttpOnly = true
                    };

                    var role = "";
                    if (userModel.Role == "Faculty" || userModel.Role == "faculty")
                    {
                        role = "faculty";
                    }
                    else {
                        role = "student";
                    }

                    Response.Cookies.Append("UserRole", role, cookieOptions);

                    return RedirectToAction("Home", "Dashboard");
                }
                else {
                    ViewBag.ErrorMessage = "Email ID does not exist. Please register email id first and then login again.";
                }

                return View("Login");
            }
            catch (FirebaseAuthException ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
                ViewBag.ErrorMessage = ex.Message;
                return View("Login");
            }

        }
    }
}

