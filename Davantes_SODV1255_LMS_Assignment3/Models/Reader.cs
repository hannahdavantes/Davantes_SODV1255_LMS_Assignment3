using System.ComponentModel.DataAnnotations;

namespace Davantes_SODV1255_LMS_Assignment3.Models {
    public class Reader {
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

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone must be exactly 10 digits")]
        public string Phone { get; set; }

        public string FormattedPhone {
            get {
                if (string.IsNullOrWhiteSpace(Phone) || Phone.Length != 10)
                    return Phone;

                return $"{Phone.Substring(0, 3)}-{Phone.Substring(3, 3)}-{Phone.Substring(6, 4)}";
            }
        }

        public List<Borrowing> Borrowings { get; set; }


        public Reader() {
            Borrowings = new List<Borrowing>();
        }

        public Reader(int id, string firstName, string lastName, string email, string phone) {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Borrowings = new List<Borrowing>();

        }
    }
}
