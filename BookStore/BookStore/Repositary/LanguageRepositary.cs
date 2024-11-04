using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repositary
{
    public class LanguageRepositary
    {
        private readonly BookStoreContext _context;
        public LanguageRepositary(BookStoreContext context)
        {

            _context = context;
        }
        public async Task<List<LanguageModel>> GetAll()
        {
            return await _context.Language.Select(x => new LanguageModel
            {
                Id = x.Id,
                Description = x.description,
                Name = x.name,

            }).ToListAsync();
        }
    }
}
