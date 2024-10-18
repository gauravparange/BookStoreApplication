using BookStore.Models;
using BookStore.Repositary;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepositary _bookRepositary;
        public BookController(BookRepositary bookRepositary)
        {
            _bookRepositary = bookRepositary;
        }
        public async Task<ViewResult> GetAllBooks()
        {
            var data = await _bookRepositary.GetAllBooks();
            return View(data);
        }
        public async Task<ViewResult> GetBook(int id)
        {
            var data = await _bookRepositary.GetBookById(id);
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
        public async Task<IActionResult> AddNewBook(BookModel model)
        {
            if (ModelState.IsValid)
            {
                int id = await _bookRepositary.AddNewBook(model);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { issuccess = true, bookId = id });
                }
            }
           // ModelState.AddModelError("", "This is my custom error message");
            return View();
        }
    }
}
