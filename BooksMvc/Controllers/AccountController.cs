using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BooksMvc.Models;
using System.Data;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace BooksMvc.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly ILogger<AccountController> _logger;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AccountController> logger)
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Register() => View(new RegisterViewModel());


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel Model)
        {
            if (!ModelState.IsValid)
            {
                return View(Model);
            }
            var user = new User()
            {
                UserName = Model.UserName,
            };

            var creation_result = await _UserManager.CreateAsync(user, Model.Password);
            if (creation_result.Succeeded)
            {
                _logger.LogInformation("Пользователь {0} зарегистрирован", user);

              //  await _UserManager.AddToRoleAsync(user, Role.Users);

                await _SignInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
            foreach (var error in creation_result.Errors)
            {
                ModelState.AddModelError(" ", error.Description);
            }
            var error_info = string.Join(", ", creation_result.Errors.Select(e => e.Description));
            _logger.LogWarning("Ошибка при регистрации пользователя {0}:{1}",
                user,
                error_info);

            return View(Model);
        }

        [AllowAnonymous]
        public IActionResult Login(string? ReturnURL) => View(new LoginViewModel { ReturnUrl = ReturnURL });

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel Model)
        {
            if (!ModelState.IsValid)
                return View(Model);

            var login_result = await _SignInManager.PasswordSignInAsync(
                Model.UserName,
                Model.Password,
                Model.RememberMe,
                lockoutOnFailure: true);

            if (login_result.Succeeded)
            {
                _logger.LogInformation("Пользователь {0} успешно вошёл в систему", Model.UserName);

                return LocalRedirect(Model.ReturnUrl ?? "/");
            }
            ModelState.AddModelError("", "Неверное имя пользователя, или пароль");

            _logger.LogWarning("Ошибка входа пользователя {0} - неверное имя, или пароль", Model.UserName);

            return View(Model);
        }
        public async Task<IActionResult> Logout()
        {
            var user_name = User.Identity!.Name;

            await _SignInManager.SignOutAsync();

            _logger.LogInformation("Пользователь {0} вышел из системы", user_name);

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl!;
            return View();
        }

    }
}

