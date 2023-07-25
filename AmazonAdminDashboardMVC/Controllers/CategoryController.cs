using AmazonAdmin.Application.Services;
using AmazonAdmin.DTO;
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
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories =await categoryService.GetAllCategory();
            ViewBag.Categories=categories;
			return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddCategoryDto categoryVm)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }
    }
}
