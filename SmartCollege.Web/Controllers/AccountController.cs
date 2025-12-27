using Microsoft.AspNetCore.Mvc;
using SmartCollege.Web.Models;
using SmartCollege.Web.Services;

namespace SmartCollege.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApiService _apiService;

        public AccountController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await _apiService.PostAsync<LoginResponse>("auth/login", model);

            if (result == null || string.IsNullOrEmpty(result.AccessToken))
            {
                model.ErrorMessage = "Invalid username or password";
                return View(model);
            }

            HttpContext.Session.SetString("JWT", result.AccessToken);
            return RedirectToAction("Index", "Students");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
