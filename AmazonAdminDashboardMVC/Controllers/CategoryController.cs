using AmazonAdmin.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AmazonAdminDashboardMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IcategoryServices categoryService;
        public CategoryController(IcategoryServices _categoryService)
        {
            categoryService = _categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var categories =await categoryService.GetAllCategory();
            return View(categories);
        }
        public Task<IActionResult> Create()
        {
            return View();
        }

	}
}
