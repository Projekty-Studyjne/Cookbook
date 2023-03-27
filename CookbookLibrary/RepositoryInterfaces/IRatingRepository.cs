using CookbookLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookLibrary.RepositoryInterfaces
{
    public interface IRatingRepository : IDisposable
    {
        IEnumerable<Rating> GetRatings();
        Rating GetRatingById(int ratingId);
        void InsertRating(Rating rating);
        void DeleteRating(int ratingId);
        void UpdateRating(Rating rating);
        void Save();
    }
}
