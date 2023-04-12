using CookbookLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookBLL.Interfaces
{
    public interface IRatingService
    {
        Task<IEnumerable<Rating>> GetAll();
        Task<Rating> GetRatingById(int ratingId);
        Task Update(Rating rating);
        Task Add(Rating rating);
        Task Delete(int ratingId);
    }
}
