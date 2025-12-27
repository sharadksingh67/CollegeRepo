using Microsoft.AspNetCore.Mvc;
using SmartCollege.Web.Models;
using SmartCollege.Web.Services;

namespace SmartCollege.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApiService _apiService;

        public StudentsController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var students =
                await _apiService.GetAsync<List<StudentViewModel>>("students");

            if (students == null)
                return RedirectToAction("Login", "Account");

            return View(students);
        }
    }
}
