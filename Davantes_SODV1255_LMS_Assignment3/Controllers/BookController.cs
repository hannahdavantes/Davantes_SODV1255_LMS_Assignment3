using Davantes_SODV1255_LMS_Assignment3.Models;
using Davantes_SODV1255_LMS_Assignment3.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Davantes_SODV1255_LMS_Assignment3.Controllers {
    public class BookController : Controller {
        [HttpGet("books")]
        public IActionResult Index() {
            //Check if there is user that is logged in then if not, redirect to login
            if (HttpContext.Session.GetInt32("UserId") == null) {
                TempData["Danger"] = "You must be logged in to access this page";
                return RedirectToAction("Login", "Home");
            }

            //Display the books list
            var books = BookRepository.GetBookList();
            return View(books);
        }

        [HttpGet("books/{id}")]
        public IActionResult Details(int id) {
            //Check if there is user that is logged in then if not, redirect to login
            if (HttpContext.Session.GetInt32("UserId") == null) {
                TempData["Danger"] = "You must be logged in to access this page";
                return RedirectToAction("Login", "Home");
            }
            //Get the book details based on ID
            var book = BookRepository.GetBookById(id);

            //If the book is not found, show an error message and redirect to the book list
            if (book == null) {
                TempData["Danger"] = "Book not found.";
                return RedirectToAction("Index");
            }

            //Show the book details
            return View(book);
        }

        [HttpGet("books/add")]
        public IActionResult Add() {
            //Check if there is user that is logged in then if not, redirect to login
            if (HttpContext.Session.GetInt32("UserId") == null) {
                TempData["Danger"] = "You must be logged in to access this page";
                return RedirectToAction("Login", "Home");
            }

            //Show page to add a book
            return View();
        }

        [HttpPost("books/add")]
        public IActionResult Add(Book book) {
            //Check if there is user that is logged in then if not, redirect to login
            if (HttpContext.Session.GetInt32("UserId") == null) {
                TempData["Danger"] = "You must be logged in to access this page";
                return RedirectToAction("Login", "Home");
            }
            //Validate the book details and if there are errors, show the form again with error messages
            if (!ModelState.IsValid) {
                return View(book);
            }

            //Add the book to the repository and show a success message, then redirect to the book list
            BookRepository.AddBook(book);
            TempData["Success"] = $"{book.Title} has been added successfully.";
            return RedirectToAction("Index");
        }

        [HttpGet("books/edit/{id}")]
        public IActionResult Edit(int id) {
            //Check if there is user that is logged in then if not, redirect to login
            if (HttpContext.Session.GetInt32("UserId") == null) {
                TempData["Danger"] = "You must be logged in to access this page";
                return RedirectToAction("Login", "Home");
            }
            //Get the book details based on ID
            var book = BookRepository.GetBookById(id);

            //If the book is not found, show an error message and redirect to the book list
            if (book == null) {
                TempData["Danger"] = "Book not found.";
                return RedirectToAction("Index");
            }
            //Show the book details in the edit form
            return View(book);
        }

        [HttpPost("books/edit/{id}")]
        public IActionResult Edit(Book book) {
            //Check if there is user that is logged in then if not, redirect to login
            if (HttpContext.Session.GetInt32("UserId") == null) {
                TempData["Danger"] = "You must be logged in to access this page";
                return RedirectToAction("Login", "Home");
            }

            //Validate the book details and if there are errors, show the form again with error messages
            if (!ModelState.IsValid) {
                return View(book);
            }

            //Update the book in the repository and show a success message, then redirect to the book list
            BookRepository.UpdateBook(book);
            TempData["Success"] = $"{book.Title} has been updated successfully.";
            return RedirectToAction("Index");
        }

        [HttpGet("books/delete/{id}")]
        public IActionResult Delete(int id) {
            //Check if there is user that is logged in then if not, redirect to login
            if (HttpContext.Session.GetInt32("UserId") == null) {
                TempData["Danger"] = "You must be logged in to access this page";
                return RedirectToAction("Login", "Home");
            }

            //Get the book details based on ID to show in the confirmation page
            var book = BookRepository.GetBookById(id);

            //If the book is not found, show an error message and redirect to the book list
            if (book == null) {
                TempData["Danger"] = "Book not found.";
                return RedirectToAction("Index");
            }

            //Show the book details in the delete confirmation page
            return View(book);
        }

        [HttpPost("books/delete/{id}")]
        public IActionResult DeleteConfirmed(int id) {
            //Check if there is user that is logged in then if not, redirect to login
            if (HttpContext.Session.GetInt32("UserId") == null) {
                TempData["Danger"] = "You must be logged in to access this page";
                return RedirectToAction("Login", "Home");
            }

            //Get the book details based on ID to show in the success message after deletion
            var book = BookRepository.GetBookById(id);

            //If the book is not found, show an error message and redirect to the book list
            if (book == null) {
                TempData["Danger"] = "Book not found.";
                return RedirectToAction("Index");
            }

            //Delete the book from the repository and show a success message, then redirect to the book list
            BookRepository.DeleteBook(id);
            TempData["Success"] = $"\"{book.Title}\" has been deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}