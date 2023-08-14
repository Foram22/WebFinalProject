using System;
using Microsoft.AspNetCore.Mvc.Filters;
using WebFinalProject.Models;

namespace WebFinalProject
{
	public class FetchUser : ActionFilterAttribute
	{

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Fetch the user model data here (e.g., from a database or authentication)
            UserModel userModel = FetchUserModel();

            // Add the user model to ViewData or ViewBag
            //context.Controller.ViewData["UserModel"] = userModel;
            base.OnActionExecuting(context);
        }


        private UserModel FetchUserModel()
        {
            // Fetch the user model data (replace with your implementation)
            // Example: Retrieve the user data from a service or database
            UserModel user = new UserModel
            {
                // Populate user properties
                // ...
            };

            return user;
        }
    }
}

