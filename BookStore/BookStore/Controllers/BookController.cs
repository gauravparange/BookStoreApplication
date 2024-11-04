using BookStore.Data;
using BookStore.Models;
using BookStore.Repositary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepositary _bookRepositary;
        private readonly LanguageRepositary _languageRepositary;
        public BookController(BookRepositary bookRepositary, LanguageRepositary languageRepositary)
        {
            _bookRepositary = bookRepositary;
            _languageRepositary = languageRepositary;
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
        public List<Books> SearchBooks(string title,string Authorname)
        {
            return _bookRepositary.SearchBook(title, Authorname);
        }
        public async Task<IActionResult> AddNewBook(bool issuccess = false, int bookId = 0)
        {
            ViewBag.IsSuccess = issuccess;
            ViewBag.bookId = bookId;


            //var group1 = new SelectListGroup() { Name = "Group1" };
            //var group2 = new SelectListGroup() { Name = "Group2" };
            //var group3 = new SelectListGroup() { Name = "Group3" };




            //ViewBag.Langauage = new List<SelectListItem>()
            //{
            //    new SelectListItem(){Text = "English",Value = "1", Group=group1},
            //    new SelectListItem(){Text = "Hindi",Value = "2", Group=group1},
            //    new SelectListItem(){Text = "Marathi",Value = "3", Group=group2},
            //    new SelectListItem(){Text = "Tamil",Value = "4", Group=group2},
            //    new SelectListItem(){Text = "Urdu",Value = "5", Group=group3},
            //    new SelectListItem(){Text = "Malayam",Value = "6", Group=group3},
            //};





            //    _bookRepositary.GetLangauges().Select(x => new SelectListItem()
            //{
            //    Text = x.text
            //});

            var model = new BookModel();
            ViewBag.Language = new SelectList(await _languageRepositary.GetAll(),"Id","Name");
            return View(model);
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
            //ViewBag.Langauage = new SelectList(_bookRepositary.GetLangauges(), "Id", "text");


            //var group1 = new SelectListGroup() { Name = "Group1" };
            //var group2 = new SelectListGroup() { Name = "Group2" };
            //var group3 = new SelectListGroup() { Name = "Group3" };




            //ViewBag.Langauage = new List<SelectListItem>()
            //{
            //    new SelectListItem(){Text = "English",Value = "1", Group=group1},
            //    new SelectListItem(){Text = "Hindi",Value = "2", Group=group1},
            //    new SelectListItem(){Text = "Marathi",Value = "3", Group=group2},
            //    new SelectListItem(){Text = "Tamil",Value = "4", Group=group2},
            //    new SelectListItem(){Text = "Urdu",Value = "5", Group=group3},
            //    new SelectListItem(){Text = "Malayam",Value = "6", Group=group3},
            //};
            ViewBag.Language = new SelectList(await _languageRepositary.GetAll(), "Id", "Name");

            return View();
        }
    }
}
