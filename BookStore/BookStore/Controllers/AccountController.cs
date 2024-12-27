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
        public async Task<IActionResult> Login(SignInModel signInModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepos.PasswordSignInAsync(signInModel);
                if (result.Succeeded)
                {
                    if(!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                if(result.IsNotAllowed)
                {

                    ModelState.AddModelError("", "Not Allowed to login");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Credentials");

                }
            }
            return View(signInModel);
        }
        [Route("logout")]
        public async Task<IActionResult> LogOut()
        {
            await _accountRepos.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [Route("change-password")]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [Route("change-password")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepos.ChangePasswordAsync(model);
                if (result.Succeeded) 
                {
                    ViewBag.IsSuccess = true;
                    ModelState.Clear();
                    return View();
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }
        [HttpGet("confirm-email")]
        public async Task ConfirmEmail(string uid,string token)
        {
            if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(token))
            {

            }
        }
    }
}
