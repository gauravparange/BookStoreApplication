using BookStore.Data;
using BookStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Repositary
{
    public interface IBookRepositary
    {
        Task<int> AddNewBook(BookModel book);
        Task<List<BookModel>> GetAllBooks();
        Task<BookModel> GetBookById(int id);
        Task<List<BookModel>> GetTopBooksAsync(int count);
        List<Books> SearchBook(string title, string authorName);
    }
}