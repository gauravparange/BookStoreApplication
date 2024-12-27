using BookStore.Models;
using System.Threading.Tasks;

namespace BookStore.Service
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions options);
        Task SendConfirmedEmail(UserEmailOptions options);
    }
}