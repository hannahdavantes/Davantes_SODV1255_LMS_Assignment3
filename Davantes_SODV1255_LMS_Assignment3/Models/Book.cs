using System.ComponentModel.DataAnnotations;

namespace Davantes_SODV1255_LMS_Assignment3.Models {
    public class Book {

        public int ID { get; set; }
        [Required(ErrorMessage = "Book Title is required")]
        [StringLength(100, ErrorMessage = "Book title cannot exceed 100 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Book author is required")]
        [StringLength(50, ErrorMessage = "Book author cannot exceed 50 characters")]
        public string Author { get; set; }
        [Required(ErrorMessage = "Book publisher is required")]
        [StringLength(50, ErrorMessage = "Book publisher cannot exceed 50 characters")]
        public string Publisher { get; set; }
        [Range(1800, 2026, ErrorMessage = "Year published must be between 1800 and 2026")]
        public string YearPublished { get; set; }
        public bool IsAvailable { get; set; }

        public Book() { }

        public Book(int iD, string title, string author, string publisher, string yearPublished, bool isAvailable) {
            ID = iD;
            Title = title;
            Author = author;
            Publisher = publisher;
            YearPublished = yearPublished;
            IsAvailable = isAvailable;
        }
    }
}
