using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace BookStore.Repositary
{
    public class BookRepositary
    {
        private readonly BookStoreContext _context;
        public BookRepositary(BookStoreContext context)
        {
            _context = context; 
        }
        public async Task<List<BookModel>> GetAllBooks()
        {
            var books = new List<BookModel>();
            var allBooks = await _context.BookModel.ToListAsync();
            if (allBooks.Any() == true)
            {
                foreach (var book in allBooks)
                {
                    books.Add(new BookModel()
                    {
                        Id = book.Id,
                        Author = book.Author,
                        Category = book.Category,
                        Description = book.Description,
                        Language = book.Language,
                        Title = book.Title,
                        TotalPages = book.TotalPages
                    });
                }
            }
            return books;
        }
        public async Task<BookModel> GetBookById(int id)
        {
            var book = await _context.BookModel.FindAsync(id);
            if (book != null)
            {
                var bookDetails = new BookModel()
                {
                    Id = book.Id,
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    Language = book.Language,
                    Title = book.Title,
                    TotalPages = book.TotalPages.HasValue ? book.TotalPages.Value : 0
                };
            }
            return _context.BookModel.Where( x => x.Id == id).FirstOrDefault();
        }
        public async Task<int>  AddNewBook(BookModel book) 
        {
            var newbook = new BookModel()
            {
                Id = book.Id,
                Author = book.Author,
                Category = book.Category,
                Description = book.Description,
                Language = book.Language,
                Title = book.Title,
                TotalPages = book.TotalPages
            };
            await _context.BookModel.AddAsync(newbook);
            await _context.SaveChangesAsync();
            return newbook.Id;
        }
        public List<BookModel> SearchBook(string title, string authorName)
        {
            return _context.BookModel.Where(x => x.Title.Contains(title) && x.Author.Contains(authorName)).ToList();

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
        public List<LanguageModel> GetLangauges()
        {
            return new List<LanguageModel>()
            {
                new LanguageModel() {Id = 1,text = "English" },
                new LanguageModel() {Id = 2,text = "Hindi" },
                new LanguageModel() {Id = 3,text = "Marathi" }
            };

        }
    }
}
