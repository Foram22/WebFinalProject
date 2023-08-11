using Firebase.Database;
using Firebase.Database.Query;
using WebFinalProject.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebFinalProject.Controllers
{
    public class RoleController : Controller
    {
        

        // GET: /<controller>/
        public ActionResult Role()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Role(string role)
        {
            if (role != null)
            {
                try
                {
                    string? serializedUser = TempData["User"] as string;
                    if (!string.IsNullOrEmpty(serializedUser))
                    {
                        UserModel userModel = JsonConvert.DeserializeObject<UserModel>(serializedUser);
                        if (userModel != null)
                        {
                            var firebaseClient = new FirebaseClient("https://facultymeets-default-rtdb.firebaseio.com/");
                            var userRef = firebaseClient.Child("users");
                            //var userToUpdate = (await userRef.Child(userModel.Id).OnceAsync<UserModel>()).FirstOrDefault();
                            //var userToUpdate = (await userRef.OrderBy("Id").EqualTo(userModel.Id).OnceAsync<UserModel>()).FirstOrDefault();
                            var userToUpdate = userRef.Child(userModel.Id);

                            if (userToUpdate != null)
                            {
                                userModel.Role = role;
                                await userToUpdate.PutAsync(userModel);
                                return RedirectToAction("Login", "Login");
                            }
                            else
                            {
                                ViewBag.SelectedRole = "User not found. Please try again later.";
                            }
                        }
                    }
                    else
                    {
                        ViewBag.SelectedRole = "User not found. Please try again later.";
                    }
                    
                }
                catch (Exception ex)
                {
                    // Handle Firebase update error
                    ModelState.AddModelError(string.Empty, "Error updating user data: " + ex.Message);
                    ViewBag.SelectedRole = ex.Message;
                }

            }
            else {
                ViewBag.SelectedRole = "Please select any role.";
            }

            return View("Role");
        }
    }
}

