using System.ComponentModel.DataAnnotations;

namespace Davantes_SODV1255_LMS_Assignment3.ViewModels {
    public class LoginViewModel {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        public LoginViewModel() {

        }
        LoginViewModel(string email, string password) {
            Email = email;
            Password = password;
        }
    }
}
