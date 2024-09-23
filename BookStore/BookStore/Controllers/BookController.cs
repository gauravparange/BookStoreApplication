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
        public BookController()
        {
            _bookRepositary = new BookRepositary();
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
    }
}
