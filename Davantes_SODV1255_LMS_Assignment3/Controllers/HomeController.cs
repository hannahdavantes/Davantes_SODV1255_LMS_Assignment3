using Davantes_SODV1255_LMS_Assignment3.Models;
using Davantes_SODV1255_LMS_Assignment3.Repositories;
using Davantes_SODV1255_LMS_Assignment3.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Davantes_SODV1255_LMS_Assignment3.Controllers {
    public class HomeController : Controller {
        [Route("/")]
        public IActionResult Index() {
            return View();
        }

        [HttpGet("/login")]
        public IActionResult Login() {
            return View(new LoginViewModel());
        }

        [HttpPost("/login")]
        public IActionResult Login(LoginViewModel model) {
            if (!ModelState.IsValid)
                return View(model);

            var user = UserRepository.Authenticate(model.Email, model.Password);

            if (user == null) {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(model);
            }

            HttpContext.Session.SetInt32("UserId", user.ID);
            HttpContext.Session.SetString("UserFullName", $"{user.FirstName} {user.LastName}");

            return RedirectToAction("Dashboard");
        }

        [HttpGet("/register")]
        public IActionResult Register() {
            return View(new RegisterViewModel());
        }

        [HttpPost("/register")]
        public IActionResult Register(RegisterViewModel model) {
            if (!ModelState.IsValid)
                return View(model);

            if (UserRepository.EmailExists(model.Email)) {
                ModelState.AddModelError("", "User already exists");
                return View(model);
            }

            var newUser = new User(
                0,
                model.FirstName,
                model.LastName,
                model.Email,
                model.Password
            );

            var savedUser = UserRepository.Add(newUser);

            HttpContext.Session.SetInt32("UserId", savedUser.ID);
            HttpContext.Session.SetString("UserFullName", $"{savedUser.FirstName} {savedUser.LastName}");

            return RedirectToAction("Dashboard");
        }


        [HttpGet("/dashboard")]
        public IActionResult Dashboard() {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login");

            var user = UserRepository.GetById(userId.Value);

            if (user == null) {
                HttpContext.Session.Clear();
                return RedirectToAction("Login");
            }

            var books = BookRepository.GetBookList();
            var readers = ReaderRepository.GetReaderList();
            var borrowings = BorrowingRepository.GetBorrowingList();

            var now = DateTime.Now;

            var model = new DashboardViewModel();

            model.UserFullName = HttpContext.Session.GetString("UserFullName");

            model.TotalBooks = books.Count;
            model.AvailableBooks = books.Count(b => b.IsAvailable);
            model.BorrowedBooks = books.Count(b => !b.IsAvailable);

            model.TotalReaders = readers.Count;

            model.ActiveBorrowings = borrowings.Count(b => b.ReturnDate == null);
            model.OverdueBorrowings = borrowings.Count(b => b.ReturnDate == null && b.DueDate < now);

            return View(model);
        }

        [HttpPost("/logout")]
        public IActionResult Logout() {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

    }
}