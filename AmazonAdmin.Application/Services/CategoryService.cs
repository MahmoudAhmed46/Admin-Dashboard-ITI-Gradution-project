using AmazonAdmin.Application.Contracts;
using AmazonAdmin.Domain;
using AmazonAdmin.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AmazonAdmin.Application.Services
{
    public class CategoryService : IcategoryServices
    {
        ICategoryReposatory _Repo;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment hosting;
        private readonly IImageService imageService;
        public CategoryService(ICategoryReposatory repo,
            IMapper mapper, IWebHostEnvironment _hosting,
            IImageService _imageService
           )
        {
            _Repo = repo;
            this.mapper = mapper;
            hosting = _hosting;
            imageService = _imageService;
        }

        public async Task<List<CategoryDTO>> GetAllCategory()
        {
            var categories = (await _Repo.GetAllAsync())
                .Where(c => c.categoryId == null).ToList();
            return mapper.Map<List<CategoryDTO>>(categories);
        }
		public async Task<IQueryable<Category>> GetAllCategoryQuarable(string searchValue)
		{
			var categories = (await _Repo.GetAllAsync())
			   .Where(c => c.categoryId == null && (string.IsNullOrEmpty(searchValue) ? true :
               (c.Name.Contains(searchValue)||c.arabicName.Contains(searchValue))));
			return categories;
		}

		public async Task<CategoryDTO> GetByIdAsync(int ID)
        {
            var category = await _Repo.GetByIdAsync(ID);
            return mapper.Map<CategoryDTO>(category);
        }


        public async Task<AddCategoryDto> CreateAsync(AddCategoryDto categoryVm)
        {
            var category = new Category()
            {
                Name = categoryVm.Name,
                arabicName = categoryVm.arabicName,
                categoryId = (categoryVm.categoryId != 0) ? categoryVm.categoryId : null,
            };

            var Categoryres = await _Repo.CreateAsync(category);

            string filename = await generateImageName(categoryVm.imageFile);
            if (filename != string.Empty)
            {
                await imageService.uploadImage(new ImageDTO()
                {
                    Name = filename,
                    categoryId = Categoryres?.Id
                });
            }
            return mapper.Map<AddCategoryDto>(Categoryres);
        }

        public async Task<AddCategoryDto> UpdateAsync(AddCategoryDto categoryVm, int id)
        {
            var category = new Category()
            {
                Id = id,
                Name = categoryVm.Name,
                arabicName = categoryVm.arabicName,
                categoryId = (categoryVm.categoryId != 0) ? categoryVm.categoryId : null,
            };
            var res = await _Repo.UpdateAsync(category);
            string filename = await generateImageName(categoryVm.imageFile);
            if (filename != string.Empty)
            {
				var imageId = imageService.getImageObjByCategoryId(id);
				await imageService.UpdateImage(new ImageDTO()
                {
                    Id = imageId,
                    Name = filename,
                    categoryId = id
                });
            }
            else if(filename != string.Empty)
            {
				await imageService.uploadImage(new ImageDTO()
				{
					Name = filename,
					categoryId = id
				});
			}

		   await _Repo.SaveChangesAsync();
			
			return mapper.Map<AddCategoryDto>(category);
        }
        private async Task<string> generateImageName(IFormFile? image)
        {
            string filename = "";
            if (image != null || image?.Length > 0)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "images");
                filename = new Guid().ToString() + "_" + image?.FileName;
                string fullpath = Path.Combine(uploads, filename);
                image?.CopyTo(new FileStream(fullpath, FileMode.Create));
            }

            return await Task.FromResult(filename);
        }

		
	}
}
