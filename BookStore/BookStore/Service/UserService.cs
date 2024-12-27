using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BookStore.Service
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpConext;
        public UserService(IHttpContextAccessor httpConext)
        {
            _httpConext = httpConext;
        }
        public string GetUserId()

        {
            return _httpConext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public bool? IsAuthenticated()
        {
            return _httpConext.HttpContext.User.Identity?.IsAuthenticated;
        }
    }
}
