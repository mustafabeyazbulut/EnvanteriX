using EnvanteriX.WebUI.Models;
using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebUI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ITokenService _tokenService;
        public AccountController(IApiClientService apiClientService,  ITokenService tokenService) : base(apiClientService)
        {
            _tokenService = tokenService;
        }

        [HttpGet]
        public ActionResult Login()
        {
            // Kullanıcı oturum açmışsa Dashboard'a yönlendir
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Dashboard", "Home");
            }
            return View(); // Oturum açmamışsa login ekranını göster
        }

        [HttpPost]
        public async Task<ActionResult> Login(string email, string password, bool rememberMe)
        {
            try
            {
                var tokenResponse = await _tokenService.LoginAsync(email, password,rememberMe);
                return RedirectToAction("Dashboard", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(); // Kullanıcıyı oturumdan çıkarır.
            HttpContext.Session.Clear();      // Session temizlenir.
            return RedirectToAction("Login", "Account"); // Login sayfasına yönlendirir.
        }
        [HttpGet]
        public ActionResult AccessDenied()
        {
            return View(); // Oturum açmamışsa login ekranını göster
        }
    }
}
