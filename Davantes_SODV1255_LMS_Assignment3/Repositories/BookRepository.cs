using Davantes_SODV1255_LMS_Assignment3.Models;

namespace Davantes_SODV1255_LMS_Assignment3.Repositories {
    public class BookRepository {
        private static List<Book> _books = new List<Book>() {
            new Book(1, "The Silent Code", "Alex Harper", "Blue Oak Press", "2018", false),
            new Book(2, "Shadows of Tomorrow", "Lena Brooks", "Northwind Publishing", "2020", false),
            new Book(3, "Echoes in the Grid", "Marcus Lee", "Ironleaf Books", "2019", true),
            new Book(4, "Refactor River", "Jamie Chen", "Maple Ridge House", "2021", true),
            new Book(5, "Design Patterns for Humans", "Priya Nair", "Cedar & Co.", "2017", false),
            new Book(6, "The Debugging Daybook", "Owen Patel", "Prairie Press", "2022", true),
            new Book(7, "Async After Dark", "Riley Morgan", "Night Owl Publishing", "2023", false),
            new Book(8, "Clean Code Atlas", "Noah Kim", "Summit Pages", "2016", true),
            new Book(9, "MVC Made Simple", "Ava Thompson", "Westwind Books", "2024", false),
            new Book(10, "SQL Trails", "Ethan Garcia", "DataPath Publishing", "2015", true),
        };


        public static void AddBook(Book book) {
            var nextID = (_books.Count == 0) ? 1 : _books.Max(book => book.ID) + 1;
            book.ID = nextID;
            _books.Add(book);
        }

        public static List<Book> GetBookList() {
            return _books;
        }

        public static Book GetBookById(int id) {
            var book = _books.FirstOrDefault(book => book.ID == id);
            if (book != null) {
                return book;
            }

            return null;
        }

        public static void UpdateBook(Book updatedBook) {
            var existingBook = _books.FirstOrDefault(book => book.ID == updatedBook.ID);
            if (existingBook != null) {
                existingBook.Title = updatedBook.Title;
                existingBook.Author = updatedBook.Author;
                existingBook.Publisher = updatedBook.Publisher;
                existingBook.YearPublished = updatedBook.YearPublished;
                existingBook.IsAvailable = updatedBook.IsAvailable;
            }
        }

        public static void DeleteBook(int id) {
            var bookToRemove = _books.FirstOrDefault(book => book.ID == id);
            if (bookToRemove != null) {
                _books.Remove(bookToRemove);
            }
        }
    }
}
