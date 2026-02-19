using Davantes_SODV1255_LMS_Assignment3.Models;
using Davantes_SODV1255_LMS_Assignment3.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Davantes_SODV1255_LMS_Assignment3.Controllers {
    public class BookController : Controller {
        [HttpGet("books")]
        public IActionResult Index() {
            if (HttpContext.Session.GetInt32("UserId") == null) {
                return RedirectToAction("Login", "Home");
            }

            var books = BookRepository.GetBookList();
            return View(books);
        }

        [HttpGet("books/{id}")]
        public IActionResult Details(int id) {
            if (HttpContext.Session.GetInt32("UserId") == null) {
                return RedirectToAction("Login", "Home");
            }

            var book = BookRepository.GetBookById(id);
            return View(book);
        }

        [HttpGet("books/add")]
        public IActionResult Add() {
            if (HttpContext.Session.GetInt32("UserId") == null) {
                return RedirectToAction("Login", "Home");
            }

            return View();
        }

        [HttpPost("books/add")]
        public IActionResult Add(Book book) {
            if (HttpContext.Session.GetInt32("UserId") == null) {
                return RedirectToAction("Login", "Home");
            }

            if (!ModelState.IsValid) {
                return View(book);
            }

            BookRepository.AddBook(book);
            return RedirectToAction("Index");
        }

        [HttpGet("books/edit/{id}")]
        public IActionResult Edit(int id) {
            if (HttpContext.Session.GetInt32("UserId") == null) {
                return RedirectToAction("Login", "Home");
            }

            var book = BookRepository.GetBookById(id);
            return View(book);
        }

        [HttpPost("books/edit/{id}")]
        public IActionResult Edit(Book book) {
            if (HttpContext.Session.GetInt32("UserId") == null) {
                return RedirectToAction("Login", "Home");
            }

            if (!ModelState.IsValid) {
                return View(book);
            }

            BookRepository.UpdateBook(book);
            return RedirectToAction("Index");
        }

        [HttpGet("books/delete/{id}")]
        public IActionResult Delete(int id) {
            if (HttpContext.Session.GetInt32("UserId") == null) {
                return RedirectToAction("Login", "Home");
            }

            var book = BookRepository.GetBookById(id);
            return View(book);
        }

        [HttpPost("books/delete/{id}")]
        public IActionResult DeleteConfirmed(int id) {
            if (HttpContext.Session.GetInt32("UserId") == null) {
                return RedirectToAction("Login", "Home");
            }

            BookRepository.DeleteBook(id);
            return RedirectToAction("Index");
        }
    }
}
