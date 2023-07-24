using AmazonAdmin.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.Application.Services
{
	public interface IcategoryServices
	{
		Task<List<CategoryDTO>> GetAllCategory();
		Task<CategoryDTO> GetByIdAsync(int ID);
        Task<List<arCategoryDTO>> GetAllCategoryInAR();
        Task<arCategoryDTO> GetByIdAsyncInAR(int ID);
    }
}
