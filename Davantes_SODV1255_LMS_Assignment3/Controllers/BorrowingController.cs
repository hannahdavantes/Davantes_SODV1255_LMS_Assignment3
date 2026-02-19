using Davantes_SODV1255_LMS_Assignment3.Models;
using Davantes_SODV1255_LMS_Assignment3.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Davantes_SODV1255_LMS_Assignment3.Controllers {
    public class BorrowingController : Controller {
        [HttpGet("borrowings")]
        public IActionResult Index() {
            if (HttpContext.Session.GetInt32("UserId") == null) {
                return RedirectToAction("Login", "Home");
            }

            var borrowings = BorrowingRepository.GetBorrowingList();

            var bookTitles = BookRepository.GetBookList()
                .ToDictionary(b => b.ID, b => b.Title);

            var readerNames = ReaderRepository.GetReaderList()
                .ToDictionary(r => r.ID, r => $"{r.FirstName} {r.LastName}");

            ViewBag.BookTitles = bookTitles;
            ViewBag.ReaderNames = readerNames;

            return View(borrowings);
        }

        [HttpGet("borrowings/{id}")]
        public IActionResult Details(int id) {
            if (HttpContext.Session.GetInt32("UserId") == null) {
                return RedirectToAction("Login", "Home");
            }

            var borrowing = BorrowingRepository.GetBorrowingById(id);
            if (borrowing == null) {
                return NotFound();
            }

            var book = BookRepository.GetBookById(borrowing.BookID);
            var reader = ReaderRepository.GetReaderById(borrowing.ReaderID);

            ViewBag.BookTitle = book?.Title ?? $"Book #{borrowing.BookID}";
            ViewBag.ReaderName = reader != null
                ? $"{reader.FirstName} {reader.LastName}"
                : $"Reader #{borrowing.ReaderID}";

            return View(borrowing);
        }

        [HttpGet("borrowings/add")]
        public IActionResult Add() {
            if (HttpContext.Session.GetInt32("UserId") == null) {
                return RedirectToAction("Login", "Home");
            }

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

            return View();
        }

        [HttpPost("borrowings/add")]
        public IActionResult Add(Borrowing borrowing) {
            if (HttpContext.Session.GetInt32("UserId") == null) {
                return RedirectToAction("Login", "Home");
            }

            if (!ModelState.IsValid) {
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

                return View(borrowing);
            }

            var added = BorrowingRepository.AddBorrowing(borrowing);

            if (!added) {
                ModelState.AddModelError("", "Unable to create borrowing. The book may not be available.");

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

                return View(borrowing);
            }

            return RedirectToAction("Index");
        }

        [HttpGet("borrowings/return/{id}")]
        public IActionResult Return(int id) {
            if (HttpContext.Session.GetInt32("UserId") == null) {
                return RedirectToAction("Login", "Home");
            }

            var borrowing = BorrowingRepository.GetBorrowingById(id);
            if (borrowing == null) {
                return NotFound();
            }

            var book = BookRepository.GetBookById(borrowing.BookID);
            var reader = ReaderRepository.GetReaderById(borrowing.ReaderID);

            ViewBag.BookTitle = book?.Title ?? $"Book #{borrowing.BookID}";
            ViewBag.ReaderName = reader != null
                ? $"{reader.FirstName} {reader.LastName}"
                : $"Reader #{borrowing.ReaderID}";

            return View(borrowing);
        }

        [HttpPost("borrowings/return/{id}")]
        public IActionResult ReturnConfirmed(int id) {
            if (HttpContext.Session.GetInt32("UserId") == null) {
                return RedirectToAction("Login", "Home");
            }

            BorrowingRepository.ReturnBook(id);
            return RedirectToAction("Index");
        }

        [HttpGet("borrowings/extend/{id}")]
        public IActionResult Extend(int id) {
            if (HttpContext.Session.GetInt32("UserId") == null) {
                return RedirectToAction("Login", "Home");
            }

            var borrowing = BorrowingRepository.GetBorrowingById(id);
            if (borrowing == null) {
                return NotFound();
            }

            var book = BookRepository.GetBookById(borrowing.BookID);
            ViewBag.BookTitle = book?.Title ?? $"Book #{borrowing.BookID}";

            return View(borrowing);
        }

        [HttpPost("borrowings/extend/{id}")]
        public IActionResult ExtendConfirmed(int id) {
            if (HttpContext.Session.GetInt32("UserId") == null) {
                return RedirectToAction("Login", "Home");
            }

            BorrowingRepository.ExtendBorrowing(id);
            return RedirectToAction("Index");
        }
    }
}
