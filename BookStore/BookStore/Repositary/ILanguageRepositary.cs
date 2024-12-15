using BookStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Repositary
{
    public interface ILanguageRepositary
    {
        Task<List<LanguageModel>> GetAll();
    }
}