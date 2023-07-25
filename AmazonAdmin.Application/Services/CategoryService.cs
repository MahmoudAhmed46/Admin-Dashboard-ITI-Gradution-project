using AmazonAdmin.Application.Contracts;
using AmazonAdmin.Domain;
using AmazonAdmin.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
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
        public CategoryService(ICategoryReposatory repo, 
			IMapper mapper, IWebHostEnvironment _hosting
           )
		{
			_Repo = repo;
			this.mapper = mapper;
			hosting= _hosting;
		}

		public async Task<List<CategoryDTO>> GetAllCategory()
		{
			var categories= (await _Repo.GetAllAsync())
				.Where(c => c.categoryId == null).ToList();
			return mapper.Map<List<CategoryDTO>>(categories);
		}

		public async Task<CategoryDTO> GetByIdAsync(int ID)
		{
			var category = await _Repo.GetByIdAsync(ID);
			return mapper.Map<CategoryDTO>(category);
		}

        public async Task<List<arCategoryDTO>> GetAllCategoryInAR()
        {
            var categories = (await _Repo.GetAllAsync())
                     .Where(c => c.categoryId == null).ToList();
            return mapper.Map<List<arCategoryDTO>>(categories);
        }

        public async Task<arCategoryDTO> GetByIdAsyncInAR(int ID)
        {
            var category = await _Repo.GetByIdAsync(ID);
            return mapper.Map<arCategoryDTO>(category);
        }

   //     public async Task<AddCategoryDto> CreateAsync(AddCategoryDto categoryVm)
   //     {
			//var category = new Category()
			//{
			//	Name=categoryVm.Name,
			//	arabicName=categoryVm.arabicName,
			//	categoryId=(categoryVm.categoryId==0)? categoryVm.categoryId:null,
   //         };
			//var res = await _Repo.CreateAsync(category);
			//if(categoryVm.File!=null && categoryVm.File.Length != 0)
			//{
   //             string filename = "";
   //             string uploads = Path.Combine(hosting.WebRootPath, "Uploads");
			//	filename = new Guid().ToString() + "_" + categoryVm.File.FileName;
   //             string fullpath = Path.Combine(uploads, filename);
   //             categoryVm.File.CopyTo(new FileStream(fullpath, FileMode.Create));
				
   //         }

   //     }
    }
}
