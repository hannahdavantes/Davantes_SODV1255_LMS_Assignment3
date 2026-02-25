using Davantes_SODV1255_LMS_Assignment3.Models;
using Davantes_SODV1255_LMS_Assignment3.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Davantes_SODV1255_LMS_Assignment3.Controllers {
    public class BorrowingController : Controller {
        [HttpGet("borrowings")]
        public IActionResult Index() {
            //Check if there is user that is logged in then if not, redirect to login
            if (HttpContext.Session.GetInt32("UserId") == null) {
                TempData["Danger"] = "You must be logged in to access this page";
                return RedirectToAction("Login", "Home");
            }

            //Display the borrowings list
            var borrowings = BorrowingRepository.GetBorrowingList();

            //Get the book id and tile into a dictionary
            var bookTitles = BookRepository.GetBookList()
                .ToDictionary(b => b.ID, b => b.Title);

            //Get the reader id and full name into a dictionary
            var readerNames = ReaderRepository.GetReaderList()
                .ToDictionary(r => r.ID, r => $"{r.FirstName} {r.LastName}");

            //Pass the book title and reader name for displaying in the borrowings list
            ViewBag.BookTitles = bookTitles;
            ViewBag.ReaderNames = readerNames;

            //Show the borrowings list
            return View(borrowings);
        }

        [HttpGet("borrowings/{id}")]
        public IActionResult Details(int id) {
            //Check if there is user that is logged in then if not, redirect to login
            if (HttpContext.Session.GetInt32("UserId") == null) {
                TempData["Danger"] = "You must be logged in to access this page";
                return RedirectToAction("Login", "Home");
            }

            //Get the borrowing details based on ID
            var borrowing = BorrowingRepository.GetBorrowingById(id);

            //If the borrowing is not found, show an error message and redirect to the borrowings list
            if (borrowing == null) {
                TempData["Danger"] = "Borrowing not found.";
                return RedirectToAction("Index");
            }

            //Get the book and reader details to show in the borrowing details page
            var book = BookRepository.GetBookById(borrowing.BookID);
            var reader = ReaderRepository.GetReaderById(borrowing.ReaderID);

            //Pass the book title and reader name for displaying in the borrowing details page
            //If the book or reader is not found, show a deafult message with the ID
            ViewBag.BookTitle = book?.Title ?? $"Book #{borrowing.BookID}";
            ViewBag.ReaderName = reader != null
                ? $"{reader.FirstName} {reader.LastName}"
                : $"Reader #{borrowing.ReaderID}";

            //Show the borrowing details
            return View(borrowing);
        }

        [HttpGet("borrowings/add")]
        public IActionResult Add() {
            //Check if there is user that is logged in then if not, redirect to login
            if (HttpContext.Session.GetInt32("UserId") == null) {
                TempData["Danger"] = "You must be logged in to access this page";
                return RedirectToAction("Login", "Home");
            }

            //Use the helper method to populate the ViewBag with readers and available books for the dropdown lists in the add borrowing form
            GetDropdownDetails();
            //Show the add borrowing form
            return View();
        }

        [HttpPost("borrowings/add")]
        public IActionResult Add(Borrowing borrowing) {
            //Check if there is user that is logged in then if not, redirect to login
            if (HttpContext.Session.GetInt32("UserId") == null) {
                TempData["Danger"] = "You must be logged in to access this page";
                return RedirectToAction("Login", "Home");
            }

            //Validate the borrowing details and if there are errors, show the form again with error messages
            if (!ModelState.IsValid) {
                GetDropdownDetails();
                return View(borrowing);
            }

            //Add the borrowing to the repository and show a success message, then redirect to the borrowings list
            var added = BorrowingRepository.AddBorrowing(borrowing);

            //If the borrowing was not added, show an error message and show the form again
            if (!added) {
                ModelState.AddModelError("", "Unable to create borrowing. The book may not be available.");
                GetDropdownDetails();
                return View(borrowing);
            }

            //Show a success message and redirect to the borrowings list
            TempData["Success"] = "Borrowing has been added successfully.";
            return RedirectToAction("Index");
        }

        [HttpGet("borrowings/return/{id}")]
        public IActionResult Return(int id) {
            //Check if there is user that is logged in then if not, redirect to login
            if (HttpContext.Session.GetInt32("UserId") == null) {
                TempData["Danger"] = "You must be logged in to access this page";
                return RedirectToAction("Login", "Home");
            }

            //Get the borrowing details based on ID to show in the confirmation page
            var borrowing = BorrowingRepository.GetBorrowingById(id);

            //If the borrowing is not found, show an error message and redirect to the borrowings list
            if (borrowing == null) {
                TempData["Danger"] = "Borrowing not found.";
                return RedirectToAction("Index");
            }

            //Get the book and reader details to show in the return confirmation page
            var book = BookRepository.GetBookById(borrowing.BookID);
            var reader = ReaderRepository.GetReaderById(borrowing.ReaderID);

            //Pass the book title and reader name for displaying in the return confirmation page
            ViewBag.BookTitle = book?.Title ?? $"Book #{borrowing.BookID}";
            ViewBag.ReaderName = reader != null
                ? $"{reader.FirstName} {reader.LastName}"
                : $"Reader #{borrowing.ReaderID}";

            //Show the borrowing details in the return confirmation page
            return View(borrowing);
        }

        [HttpPost("borrowings/return/{id}")]
        public IActionResult ReturnConfirmed(int id) {
            //Check if there is user that is logged in then if not, redirect to login
            if (HttpContext.Session.GetInt32("UserId") == null) {
                TempData["Danger"] = "You must be logged in to access this page";
                return RedirectToAction("Login", "Home");
            }

            //Get the borrowing details based on ID to show in the success message after returning
            var borrowing = BorrowingRepository.GetBorrowingById(id);

            //If the borrowing is not found, show an error message and redirect to the borrowings list
            if (borrowing == null) {
                TempData["Danger"] = "Borrowing not found.";
                return RedirectToAction("Index");
            }

            //Return the book and show a success message, then redirect to the borrowings list
            BorrowingRepository.ReturnBook(id);
            TempData["Success"] = "Book has been returned successfully.";
            return RedirectToAction("Index");
        }

        [HttpGet("borrowings/extend/{id}")]
        public IActionResult Extend(int id) {
            //Check if there is user that is logged in then if not, redirect to login
            if (HttpContext.Session.GetInt32("UserId") == null) {
                TempData["Danger"] = "You must be logged in to access this page";
                return RedirectToAction("Login", "Home");
            }

            //Get the borrowing details based on ID to show in the confirmation page
            var borrowing = BorrowingRepository.GetBorrowingById(id);

            //If the borrowing is not found, show an error message and redirect to the borrowings list
            if (borrowing == null) {
                TempData["Danger"] = "Borrowing not found.";
                return RedirectToAction("Index");
            }

            //Get the book details to show in the extend confirmation page
            var book = BookRepository.GetBookById(borrowing.BookID);
            ViewBag.BookTitle = book?.Title ?? $"Book #{borrowing.BookID}";

            //Show the borrowing details in the extend confirmation page
            return View(borrowing);
        }

        [HttpPost("borrowings/extend/{id}")]
        public IActionResult ExtendConfirmed(int id) {
            //Check if there is user that is logged in then if not, redirect to login
            if (HttpContext.Session.GetInt32("UserId") == null) {
                TempData["Danger"] = "You must be logged in to access this page";
                return RedirectToAction("Login", "Home");
            }

            //Get the borrowing details based on ID to show in the success message after extending
            var borrowing = BorrowingRepository.GetBorrowingById(id);

            //If the borrowing is not found, show an error message and redirect to the borrowings list
            if (borrowing == null) {
                TempData["Danger"] = "Borrowing not found.";
                return RedirectToAction("Index");
            }

            //Extend the borrowing and show a success message, then redirect to the borrowings list
            BorrowingRepository.ExtendBorrowing(id);
            TempData["Success"] = "Borrowing has been extended successfully.";
            return RedirectToAction("Index");
        }

        //Helper method to populate the ViewBag with readers and available books
        private void GetDropdownDetails() {
            var readers = ReaderRepository.GetReaderList()
                .Select(r => new {
                    r.ID,
                    FullName = r.FirstName + " " + r.LastName
                })
                .ToList();

            ViewBag.Readers = new SelectList(readers, "ID", "FullName");

            ViewBag.AvailableBooks = new SelectList(
                BookRepository.GetBookList().Where(b => b.IsAvailable).ToList(),
                "ID",
                "Title"
            );
        }
    }
}