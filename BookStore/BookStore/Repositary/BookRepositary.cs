using BookStore.Data;
using BookStore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace BookStore.Repositary
{
    public class BookRepositary
    {
        private readonly BookStoreContext _context;
        public BookRepositary(BookStoreContext context)
        {
            _context = context; 
        }
        public List<BookModel> GetAllBooks()
        {
            return Datasource();
        }
        public BookModel GetBookById(int id)
        {
            return Datasource().Where( x => x.Id == id).FirstOrDefault();
        }
        public int AddNewBook(BookModel book) 
        {
            var newbook = new BookModel()
            {
                Author = book.Author,
                Description = book.Description,
                Title = book.Title,
                TotalPages = book.TotalPages
            };
            _context.BookModel.Add(newbook);
            _context.SaveChanges();
            return newbook.Id;
        }
        public List<BookModel> SearchBook(string title, string authorName)
        {
            return Datasource().Where(x => x.Title.Contains(title) && x.Author.Contains(authorName)).ToList();

        }
        private List<BookModel> Datasource()
        {
            return new List<BookModel>()
           {
               new BookModel() {Id = 1, Title = "MVC", Author = "Gaurav" , Description = "This is the descriprion for MVC book by Gaurav.",Category="Programming",Language="English",TotalPages=134},
               new BookModel() {Id = 2, Title = "Dot Net Core", Author = "Gaurav", Description = "This is the descriprion for Dot Net Core book by Gaurav." ,Category="Programming",Language="English",TotalPages=200},
               new BookModel() {Id = 3, Title = "C#", Author = "Shifa", Description = "This is the descriprion for C# book by Shifa." ,Category="Programming",Language="English",TotalPages=300},
               new BookModel() {Id = 4, Title = "Java", Author = "Suraj" , Description = "This is the descriprion for Java book by Suraj.",Category="Programming",Language="English",TotalPages=150},
               new BookModel() {Id = 5, Title = "Php", Author = "Shekhar" , Description = "This is the descriprion for Php book by Shekhar.",Category="Programming",Language="English",TotalPages=180},
            };
        }
    }
}
