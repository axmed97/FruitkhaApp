using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.DTOs;
using WebUI.Models;
using WebUI.Service;

namespace WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly SignInManager<AppUser> _signInManager;
        public AuthController(UserManager<AppUser> userManager, IEmailService emailService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _emailService = emailService;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null) 
            {
                ModelState.AddModelError("error", "Email or Password is not valid!");
                return View(); 
            }

            Microsoft.AspNetCore.Identity.SignInResult result =
                await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("error", "Email or Password is not valid!");
                return View();
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> Confirm(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null) return NotFound();

            var decodeToken = Uri.UnescapeDataString(token);
            var result = await _userManager.ConfirmEmailAsync(user, decodeToken);
            if(result.Succeeded)
            {
                user.EmailConfirmed = true;
                return View();
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("error", item.Description);
                }
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO register)
        {
            if(!ModelState.IsValid) return View();

            AppUser appUser = new()
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email,
                UserName = register.Email,
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(appUser, register.Password);

            if (result.Succeeded)
            {
                var gentoken = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                var link = Url.Action("Confirm", "Auth", new { userId = appUser.Id, token = gentoken }, protocol: HttpContext.Request.Scheme);
                _emailService.EmailSender(appUser.Email, link, "Confirm Emial");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("error", error.Description);
                }
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
