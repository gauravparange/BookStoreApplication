using BookStore.Models;
using BookStore.Repositary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
                return RedirectToAction("ConfirmEmail", new {email = userModel.Email});

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
                else if(result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Account blocked! Try again after some time.");

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
        public async Task<IActionResult> ConfirmEmail(string uid,string token,string email)
        {
            EmailConfirmedModel model = new EmailConfirmedModel()
            {
                Email = email
            };
            if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(token))
            {
                token = token.Replace(' ', '+');
              var result = await _accountRepos.ConfirmEmailAsync(uid, token);
                if (result.Succeeded)
                {
                    model.EmailVerified = true;
                }
            }
            return View(model);
        }
        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(EmailConfirmedModel model)
        {
           var user = await _accountRepos.GetUserByEmailAsync(model.Email);
            if (user != null)
            {
                if (user.EmailConfirmed)
                {
                    model.IsConfirmed = true;
                    return View(model);
                }
                await _accountRepos.GenerateEmailConfirmationTokenAsync(user);
                model.EmailSent = true;
                ModelState.Clear();
            }
            else
            {
                ModelState.AddModelError("","Something went wrong.");
            }
            return View(model);
        }
        [AllowAnonymous, HttpGet("forgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [AllowAnonymous, HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _accountRepos.GetUserByEmailAsync(model.Email);
                if (user != null)
                {
                    await _accountRepos.GenerateForgotPasswordTokenAsync(user);
                }
                ModelState.Clear();
                model.EmailSent = true;
            }
            return View(model);
        }
        [AllowAnonymous, HttpGet("reset-password")]
        public IActionResult ResetPassword(string uid,string token)
        {
            ResetPasswordModel model = new ResetPasswordModel()
            {
                Token = token,
                UserId = uid
            };
            return View(model);
        }
        [AllowAnonymous, HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                model.Token = model.Token.Replace(' ', '+');
                var result = await _accountRepos.ResetPasswordAsync(model);
                if (result.Succeeded)
                {
                    ModelState.Clear();
                    model.IsSuccess = true;
                    return View(model);
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
              
            }
            return View(model);
        }

    }
}
