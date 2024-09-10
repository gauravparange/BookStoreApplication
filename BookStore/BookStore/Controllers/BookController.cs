using BookStore.Models;
using BookStore.Repositary;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepositary _bookRepositary;
        public BookController()
        {
            _bookRepositary = new BookRepositary();
        }
        public List<BookModel> GetAllBooks()
        {
            return _bookRepositary.GetAllBooks();
        }
        public List<BookModel> SearchBooks(string title,string Authorname)
        {
            return _bookRepositary.SearchBook(title, Authorname);
        }
    }
}
