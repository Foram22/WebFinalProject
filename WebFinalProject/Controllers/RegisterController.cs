using Firebase.Auth;
using Firebase.Database;
using Newtonsoft.Json;
using Firebase.Database.Query;
using Microsoft.AspNetCore.Mvc;
using WebFinalProject.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebFinalProject.Controllers
{
    public class RegisterController : Controller
    {

        FirebaseAuthProvider auth;

        public RegisterController()
        {
            auth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyCGXTsFk3C8W5k8pA2rLmbv6jag8CJ95_8"));
        }
        
        // GET: /<controller>/
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(string name, string email, string password)
        {
            try
            {
                //create the user
                var authResult = await auth.CreateUserWithEmailAndPasswordAsync(email, password);

                var userId = authResult.User.LocalId;

                var firebaseClient = new FirebaseClient("https://facultymeets-default-rtdb.firebaseio.com/");
                var users = firebaseClient.Child("users");

                var newUser = new UserModel {
                    Name = name,
                    Email = email,
                    Password = password,
                    Role = "Student"
                };
                users.Child(userId).PutAsync(newUser);

                newUser.Id = userId;

                TempData["User"] = JsonConvert.SerializeObject(newUser);

                return RedirectToAction("Role", "Role");
            }
            catch (FirebaseAuthException ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
                return View();
            }

        }
    }
}

