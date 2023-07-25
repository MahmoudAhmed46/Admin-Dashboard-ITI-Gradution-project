using Amazon.Application.Contracts;
using Amazon.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Application.Services
{
    public interface IRatingService
    {
        Task<List<RatingDTO>> GetAllByProductIdAsync(int productId);
        Task<RatingDTO> createRating(RatingDTO rating);
    }
}
