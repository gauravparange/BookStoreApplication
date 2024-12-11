using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace BookStore.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options)     
        {
             
        }

        public DbSet<Books> Books { get; set; }
        public DbSet<BookGallery> BookGallery { get; set; }
        public DbSet<Language> Language { get; set; }
        
    }
}
