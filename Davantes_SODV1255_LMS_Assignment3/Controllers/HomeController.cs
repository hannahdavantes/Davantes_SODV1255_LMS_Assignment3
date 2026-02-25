using Davantes_SODV1255_LMS_Assignment3.Models;
using Davantes_SODV1255_LMS_Assignment3.Repositories;
using Davantes_SODV1255_LMS_Assignment3.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Davantes_SODV1255_LMS_Assignment3.Controllers {
    public class HomeController : Controller {
        [Route("/")]
        public IActionResult Index() {
            //Show home page
            return View();
        }

        [HttpGet("/login")]
        public IActionResult Login() {
            //Show login page
            return View(new LoginViewModel());
        }

        [HttpPost("/login")]
        public IActionResult Login(LoginViewModel model) {
            //Validate input
            if (!ModelState.IsValid) {
                return View(model);
            }

            //Authenticate user
            var user = UserRepository.Authenticate(model.Email, model.Password);

            //If authentication fails, show error
            if (user == null) {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(model);
            }

            //If authentication succeeds, store user info in session and redirect to dashboard
            HttpContext.Session.SetInt32("UserId", user.ID);
            HttpContext.Session.SetString("UserFullName", $"{user.FirstName} {user.LastName}");

            //Redirect to dashboard
            return RedirectToAction("Dashboard");
        }

        [HttpGet("/register")]
        public IActionResult Register() {
            //Show registration page
            return View(new RegisterViewModel());
        }

        [HttpPost("/register")]
        public IActionResult Register(RegisterViewModel model) {
            //Validate input
            if (!ModelState.IsValid) {
                return View(model);
            }

            //Check if email already exists
            if (UserRepository.EmailExists(model.Email)) {
                ModelState.AddModelError("", "User already exists");
                return View(model);
            }

            //Create new user and save to repository
            var newUser = new User(
                0,
                model.FirstName,
                model.LastName,
                model.Email,
                model.Password
            );

            //Save user and get assigned ID
            var savedUser = UserRepository.Add(newUser);

            //Store user info in session and redirect to dashboard
            HttpContext.Session.SetInt32("UserId", savedUser.ID);
            HttpContext.Session.SetString("UserFullName", $"{savedUser.FirstName} {savedUser.LastName}");

            TempData["Success"] = "Registration successful. You are now logged in.";
            return RedirectToAction("Dashboard");
        }


        [HttpGet("/dashboard")]
        public IActionResult Dashboard() {
            //Check if user is logged in
            var userId = HttpContext.Session.GetInt32("UserId");

            //If not logged in, redirect to login page with error message   
            if (userId == null) {
                TempData["Danger"] = "You must be logged in to access this page";
                return RedirectToAction("Login");
            }

            //Get user info from repository to verify session data is valid
            var user = UserRepository.GetById(userId.Value);

            //If user not found, clear session and redirect to login page
            if (user == null) {
                HttpContext.Session.Clear();
                return RedirectToAction("Login");
            }

            //Get data for dashboard
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
            //Clear session and redirect to home page
            HttpContext.Session.Clear();
            TempData["Success"] = "You have been logged out.";
            return RedirectToAction("Index");
        }

    }
}