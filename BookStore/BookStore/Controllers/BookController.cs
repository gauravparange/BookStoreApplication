using BookStore.Models;
using BookStore.Repositary;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.WebSockets;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepositary _bookRepositary;
        public BookController(BookRepositary bookRepositary)
        {
            _bookRepositary = bookRepositary;
        }
        public ViewResult GetAllBooks()
        {
            var data = _bookRepositary.GetAllBooks();
            return View(data);
        }
        public ViewResult GetBook(int id)
        {
            var data = _bookRepositary.GetBookById(id);
            return View(data);
        }
        public List<BookModel> SearchBooks(string title,string Authorname)
        {
            return _bookRepositary.SearchBook(title, Authorname);
        }
        public ViewResult AddNewBook(bool issuccess = false, int bookId = 0)
        {
            ViewBag.IsSuccess = issuccess;
            ViewBag.bookId = bookId;
            return View();
        }
        [HttpPost]
        public IActionResult AddNewBook(BookModel model)
        {
            int id = _bookRepositary.AddNewBook(model);
            if (id > 0)
            {
                return RedirectToAction(nameof(AddNewBook), new {issuccess = true,bookId = id});    
            }
            return View();
        }
    }
}
