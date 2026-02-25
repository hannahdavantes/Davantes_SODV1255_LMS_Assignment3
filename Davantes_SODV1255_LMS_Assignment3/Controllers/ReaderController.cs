using Davantes_SODV1255_LMS_Assignment3.Models;
using Davantes_SODV1255_LMS_Assignment3.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Davantes_SODV1255_LMS_Assignment3.Controllers {
    public class ReaderController : Controller {
        [HttpGet("readers")]
        public IActionResult Index() {
            //Check if there is user that is logged in then if not, redirect to login
            if (HttpContext.Session.GetInt32("UserId") == null) {
                TempData["Danger"] = "You must be logged in to access this page";
                return RedirectToAction("Login", "Home");
            }

            //Display the readers list
            var readers = ReaderRepository.GetReaderList();
            return View(readers);
        }

        [HttpGet("readers/{id}")]
        public IActionResult Details(int id) {
            //Check if there is user that is logged in then if not, redirect to login
            if (HttpContext.Session.GetInt32("UserId") == null) {
                TempData["Danger"] = "You must be logged in to access this page";
                return RedirectToAction("Login", "Home");
            }

            //Get the reader details based on ID
            var reader = ReaderRepository.GetReaderById(id);

            //If the reader is not found, show an error message and redirect to the reader list
            if (reader == null) {
                TempData["Danger"] = "Reader not found.";
                return RedirectToAction("Index");
            }

            //Show the reader details
            return View(reader);
        }

        [HttpGet("readers/add")]
        public IActionResult Add() {
            //Check if there is user that is logged in then if not, redirect to login
            if (HttpContext.Session.GetInt32("UserId") == null) {
                TempData["Danger"] = "You must be logged in to access this page";
                return RedirectToAction("Login", "Home");
            }

            //Show page to add a reader
            return View();
        }

        [HttpPost("readers/add")]
        public IActionResult Add(Reader reader) {
            //Check if there is user that is logged in then if not, redirect to login
            if (HttpContext.Session.GetInt32("UserId") == null) {
                TempData["Danger"] = "You must be logged in to access this page";
                return RedirectToAction("Login", "Home");
            }

            //Validate the reader details and if there are errors, show the form again with error messages
            if (!ModelState.IsValid) {
                return View(reader);
            }

            //Add the reader to the repository and show a success message, then redirect to the reader list
            ReaderRepository.AddReader(reader);
            TempData["Success"] = $"\"{reader.FirstName} {reader.LastName}\" has been added successfully.";
            return RedirectToAction("Index");
        }

        [HttpGet("readers/edit/{id}")]
        public IActionResult Edit(int id) {
            //Check if there is user that is logged in then if not, redirect to login
            if (HttpContext.Session.GetInt32("UserId") == null) {
                TempData["Danger"] = "You must be logged in to access this page";
                return RedirectToAction("Login", "Home");
            }

            //Get the reader details based on ID
            var reader = ReaderRepository.GetReaderById(id);

            //If the reader is not found, show an error message and redirect to the reader list
            if (reader == null) {
                TempData["Danger"] = "Reader not found.";
                return RedirectToAction("Index");
            }

            //Show the reader details in the edit form
            return View(reader);
        }

        [HttpPost("readers/edit/{id}")]
        public IActionResult Edit(Reader reader) {
            //Check if there is user that is logged in then if not, redirect to login
            if (HttpContext.Session.GetInt32("UserId") == null) {
                TempData["Danger"] = "You must be logged in to access this page";
                return RedirectToAction("Login", "Home");
            }

            //Validate the reader details and if there are errors, show the form again with error messages
            if (!ModelState.IsValid) {
                return View(reader);
            }

            //Update the reader in the repository and show a success message, then redirect to the reader list
            ReaderRepository.UpdateReader(reader);
            TempData["Success"] = $"\"{reader.FirstName} {reader.LastName}\" has been updated successfully.";
            return RedirectToAction("Index");
        }

        [HttpGet("readers/delete/{id}")]
        public IActionResult Delete(int id) {
            //Check if there is user that is logged in then if not, redirect to login
            if (HttpContext.Session.GetInt32("UserId") == null) {
                TempData["Danger"] = "You must be logged in to access this page";
                return RedirectToAction("Login", "Home");
            }

            //Get the reader details based on ID to show in the confirmation page
            var reader = ReaderRepository.GetReaderById(id);

            //If the reader is not found, show an error message and redirect to the reader list
            if (reader == null) {
                TempData["Danger"] = "Reader not found.";
                return RedirectToAction("Index");
            }

            //Show the reader details in the delete confirmation page
            return View(reader);
        }

        [HttpPost("readers/delete/{id}")]
        public IActionResult DeleteConfirmed(int id) {
            //Check if there is user that is logged in then if not, redirect to login
            if (HttpContext.Session.GetInt32("UserId") == null) {
                TempData["Danger"] = "You must be logged in to access this page";
                return RedirectToAction("Login", "Home");
            }

            //Get the reader details based on ID to show in the success message after deletion
            var reader = ReaderRepository.GetReaderById(id);

            //If the reader is not found, show an error message and redirect to the reader list
            if (reader == null) {
                TempData["Danger"] = "Reader not found.";
                return RedirectToAction("Index");
            }

            //Delete the reader from the repository and show a success message, then redirect to the reader list
            ReaderRepository.DeleteReader(id);
            TempData["Success"] = $"\"{reader.FirstName} {reader.LastName}\" has been deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}