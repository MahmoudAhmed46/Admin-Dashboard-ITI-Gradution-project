using AmazonAdmin.Application.Services;
using AmazonAdmin.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmazonAdminDashboardMVC.Controllers
{
	[Authorize(Roles = "Admin")]
	public class ProductController : Controller
    {
        private readonly IProductServices _services;
        private readonly ISubcategoryServices _catservice;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImageService _imageservice;
        private readonly IMapper _Mapper;

        public ProductController(IProductServices services, ISubcategoryServices categoryservice, IWebHostEnvironment webHostEnvironment, IImageService imageService,IMapper mapper)
        {
            _services = services;
            _catservice = categoryservice;
            _webHostEnvironment = webHostEnvironment;
            _imageservice = imageService;
            _Mapper=mapper;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _services.GetAllProducts());
        }

        public async Task<IActionResult> AddProduct()
        {
            List<SubCategoryDTO> categories = await _catservice.GetAllSubcategories();
            AddUpdateProductDTO createProduct = new AddUpdateProductDTO();
            createProduct.SubCategories = categories;
            return View(createProduct);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddUpdateProductDTO createProductDTO,
                                          List<IFormFile> image)
        {

            if (ModelState.IsValid)
            {
                createProductDTO.Status = true;
                var result = await _services.CreateProduct(createProductDTO);
                if (result != null)
                {
                    foreach (var item in image)
                    {
						if (item == null || item.Length == 0)
						{
							createProductDTO.SubCategories = await _catservice.GetAllSubcategories();
							return View(createProductDTO);
						}
						string uploadPath = Path.Combine("G:/GraduatedProgect/", "uploadedImages");
                        string filname = new Guid().ToString() + "_" + item.FileName;
                        string fullPath = Path.Combine(uploadPath, filname);

                        using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
                        {
                            await item.CopyToAsync(fileStream);
                        }



                        await _imageservice.uploadImage(new ImageDTO() { Name = filname, ProductID = result.Id });
                    }

                    return RedirectToAction("Index");
                }
            }
            List<SubCategoryDTO> categories = await _catservice.GetAllSubcategories();
            createProductDTO.SubCategories = categories;
            return View(createProductDTO);
        }

        public async Task<IActionResult> UpdateProduct(int id)
        {
            var productModel = await _services.GetProductsById(id);
            AddUpdateProductDTO productDTO = _Mapper.Map<AddUpdateProductDTO>(productModel);
            List<SubCategoryDTO> categories = await _catservice.GetAllSubcategories();
            productDTO.imageDTOs = await _imageservice.gitImagesByProdId(id);
            productDTO.SubCategories = categories;
            return View(productDTO);
        }


		[HttpPost]
		public async Task<IActionResult> UpdateProduct(AddUpdateProductDTO createProductDTO,int id, List<IFormFile> image)
		{
			if (ModelState.IsValid)
			{


				var result = await _services.UpdateProduct(id,createProductDTO);
				if (result)
                {

                    foreach (var item in image)
                    {
                        if (item == null || item.Length == 0)
                        {
                            createProductDTO.SubCategories = await _catservice.GetAllSubcategories();
                            return View(createProductDTO);
                        }
                        string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                        string filname = new Guid().ToString() + "_" + item.FileName;
                        string fullPath = Path.Combine(uploadPath, filname);

                        using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
                        {
                            await item.CopyToAsync(fileStream);
                        }



                        await _imageservice.uploadImage(new ImageDTO() { Name = filname, ProductID = id });
                    }
                    return RedirectToAction("Index");
                }
			}

			createProductDTO.SubCategories = await _catservice.GetAllSubcategories();
			
			return View();
		}
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var res=await _services.SoftDelete(id);
                return RedirectToAction("Index");
		}





	}
}
