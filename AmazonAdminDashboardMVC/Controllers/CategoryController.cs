using AmazonAdmin.Application.Services;
using AmazonAdmin.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AmazonAdminDashboardMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IcategoryServices categoryService;
        private readonly IImageService imageService;
        private readonly IMapper _Mapper;
        public CategoryController(IcategoryServices _categoryService,
            IMapper Mapper,
            IImageService _imageService)
        {
            categoryService = _categoryService;
            _Mapper = Mapper;
            imageService = _imageService;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await categoryService.GetAllCategory();
            return View(categories);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await categoryService.GetAllCategory();
            AddCategoryDto categoryDto = new AddCategoryDto();
            categoryDto.Categories = categories;
            return View(categoryDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddCategoryDto categoryVm)
        {

            if (ModelState.IsValid)
            {
                var resultCategory = await categoryService.CreateAsync(categoryVm);
                if (resultCategory != null)
                {
                    return RedirectToAction("Index");
                }
            }
            var categories = await categoryService.GetAllCategory();
            categoryVm.Categories = categories;
            return View(categoryVm);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var categoryRes = await categoryService.GetByIdAsync(id);
            var categories = await categoryService.GetAllCategory();
            AddCategoryDto categoryDto = _Mapper.Map<AddCategoryDto>(categoryRes);
            categoryDto.Categories = categories;
            categoryDto.ImageUrl = await imageService.getImageByCategoryId(id);
            return View(categoryDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AddCategoryDto categoryvm, int id)
        {
            if (ModelState.IsValid)
            {
                var result = await categoryService.UpdateAsync(categoryvm, id);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
