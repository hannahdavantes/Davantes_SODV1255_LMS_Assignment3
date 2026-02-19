using Davantes_SODV1255_LMS_Assignment3.Models;
using Davantes_SODV1255_LMS_Assignment3.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Davantes_SODV1255_LMS_Assignment3.Controllers {
    public class ReaderController : Controller {
        [HttpGet("readers")]
        public IActionResult Index() {
            if (HttpContext.Session.GetInt32("UserId") == null) {
                return RedirectToAction("Login", "Home");
            }

            var readers = ReaderRepository.GetReaderList();
            return View(readers);
        }

        [HttpGet("readers/{id}")]
        public IActionResult Details(int id) {
            if (HttpContext.Session.GetInt32("UserId") == null) {
                return RedirectToAction("Login", "Home");
            }

            var reader = ReaderRepository.GetReaderById(id);
            return View(reader);
        }

        [HttpGet("readers/add")]
        public IActionResult Add() {
            if (HttpContext.Session.GetInt32("UserId") == null) {
                return RedirectToAction("Login", "Home");
            }

            return View();
        }

        [HttpPost("readers/add")]
        public IActionResult Add(Reader reader) {
            if (HttpContext.Session.GetInt32("UserId") == null) {
                return RedirectToAction("Login", "Home");
            }

            if (!ModelState.IsValid) {
                return View(reader);
            }

            ReaderRepository.AddReader(reader);
            return RedirectToAction("Index");
        }

        [HttpGet("readers/edit/{id}")]
        public IActionResult Edit(int id) {
            if (HttpContext.Session.GetInt32("UserId") == null) {
                return RedirectToAction("Login", "Home");
            }

            var reader = ReaderRepository.GetReaderById(id);
            return View(reader);
        }

        [HttpPost("readers/edit/{id}")]
        public IActionResult Edit(Reader reader) {
            if (HttpContext.Session.GetInt32("UserId") == null) {
                return RedirectToAction("Login", "Home");
            }

            if (!ModelState.IsValid) {
                return View(reader);
            }

            ReaderRepository.UpdateReader(reader);
            return RedirectToAction("Index");
        }

        [HttpGet("readers/delete/{id}")]
        public IActionResult Delete(int id) {
            if (HttpContext.Session.GetInt32("UserId") == null) {
                return RedirectToAction("Login", "Home");
            }

            var reader = ReaderRepository.GetReaderById(id);
            return View(reader);
        }

        [HttpPost("readers/delete/{id}")]
        public IActionResult DeleteConfirmed(int id) {
            if (HttpContext.Session.GetInt32("UserId") == null) {
                return RedirectToAction("Login", "Home");
            }

            ReaderRepository.DeleteReader(id);
            return RedirectToAction("Index");
        }
    }
}
