using System.ComponentModel.DataAnnotations;

namespace Davantes_SODV1255_LMS_Assignment3.Models {
    public class User {
        public int ID { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
        public string Password { get; set; }

        public User() {
            
        }

        public User(int id, string firstName, string lastName, string email, string password) {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }

    }
}
