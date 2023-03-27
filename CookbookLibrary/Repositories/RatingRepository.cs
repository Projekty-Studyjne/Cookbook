using CookbookLibrary.Entities;
using CookbookLibrary.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookLibrary.Repositories
{
    public class RatingRepository : IRatingRepository, IDisposable
    {
        private CookbookDbContext context;

        public RatingRepository(CookbookDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Rating> GetRatings()
        {
            return context.Ratings.ToList();
        }

        public Rating GetRatingById(int id)
        {
            return context.Ratings.Find(id);
        }

        public void InsertRating(Rating rating)
        {
            context.Ratings.Add(rating);
        }

        public void DeleteRating(int ratingID)
        {
            Rating rating = context.Ratings.Find(ratingID);
            context.Ratings.Remove(rating);
        }

        public void UpdateRating(Rating rating)
        {
            context.Entry(rating).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
