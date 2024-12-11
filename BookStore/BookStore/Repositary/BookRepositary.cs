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
            var allBooks = await _context.Books.ToListAsync();
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
                        LanguageId = book.LanguageId,
                        Language =  _context.Language.Where(x => x.Id == book.LanguageId).FirstOrDefault().name,
                        Title = book.Title,
                        TotalPages = book.TotalPages,
                        CoverImageUrl = book.CoverImageUrl

                    });
                }
            }
            return books;
        }
        public async Task<BookModel> GetBookById(int id)
        {
            return await _context.Books.Where(x => x.Id == id).Select(book => new BookModel()
            {
                Id = book.Id,
                Author = book.Author,
                Category = book.Category,
                Description = book.Description,
                LanguageId = book.LanguageId,
                Language = book.Language.name,
                Title = book.Title,
                TotalPages = book.TotalPages.HasValue ? book.TotalPages.Value : 0,
                CoverImageUrl = book.CoverImageUrl,
                Gallery = book.bookGallery.Select(g => new GalleryModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                    URL = g.URL,
                }).ToList(),
                BookPdfUrl = book.BookPdfUrl

            }).FirstOrDefaultAsync();
        }
        public async Task<int>  AddNewBook(BookModel book) 
        {
            var newbook = new Books()
            {
                Id = book.Id,
                Author = book.Author,
                Category = book.Category,
                Description = book.Description,
                LanguageId = book.LanguageId,
                Title = book.Title,
                TotalPages = book.TotalPages,
                CoverImageUrl = book.CoverImageUrl,
                BookPdfUrl = book.BookPdfUrl
            };
            newbook.bookGallery = new List<BookGallery>();
            foreach (var item in book.Gallery)
            {
                newbook.bookGallery.Add(new BookGallery()
                {
                    Name = item.Name,
                    URL = item.URL
                });
            }
            await _context.Books.AddAsync(newbook);
            await _context.SaveChangesAsync();
            return newbook.Id;
        }
        public List<Books> SearchBook(string title, string authorName)
        {
            return _context.Books.Where(x => x.Title.Contains(title) && x.Author.Contains(authorName)).ToList();

        }
        
      
    }
}
