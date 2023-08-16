using System;
using System.ComponentModel.DataAnnotations;
using Firebase.Database;

namespace WebFinalProject.Models
{
	public class UserModel
	{
        public string Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        public string? Role { get; set; }

        public static implicit operator FirebaseObject<object>(UserModel v)
        {
            throw new NotImplementedException();
        }

        //public bool IsFaculty { get; set; }
    }
}

