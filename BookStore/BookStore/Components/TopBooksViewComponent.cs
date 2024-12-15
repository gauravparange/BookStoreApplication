using BookStore.Repositary;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.Components
{
    public class TopBooksViewComponent : ViewComponent
    {
        private readonly IBookRepositary _bookRepositary;
        public TopBooksViewComponent(IBookRepositary bookRepositary)
        {
            _bookRepositary = bookRepositary;
        }
        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            var books = await _bookRepositary.GetTopBooksAsync(count);
            return View(books);
        }
    }
}
