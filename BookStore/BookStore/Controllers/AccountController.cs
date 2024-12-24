using BookStore.Models;
using BookStore.Repositary;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepositary _accountRepos;
        public AccountController(IAccountRepositary accountRepos)
        {
            _accountRepos = accountRepos;
        }
        [Route("signup")]
        public IActionResult SignUp()
        {
            return View();
        } 
        [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpUserModel userModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepos.CreateUserAsync(userModel);
                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View(userModel);
                }

                ModelState.Clear();
                return View();

            }
            return View(userModel);
        }
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(SignInModel signInModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepos.PasswordSignInAsync(signInModel);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid Credentials");
            }
            return View(signInModel);
        }
        [Route("logout")]
        public async Task<IActionResult> LogOut()
        {
            await _accountRepos.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
