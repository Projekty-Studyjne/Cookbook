using CookbookBLL.Interfaces;
using CookbookLibrary.Entities;
using CookbookLibrary.RepositoryInterfaces;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookBLL
{
    public class RatingService : IRatingService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RatingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Rating>> GetAll()
        {
            var rating = await _unitOfWork.RatingRepository.GetAsync(includeProperties: "Comment,User,Recipe");
            return rating;
        }

        public async Task<Rating> GetRatingById(int ratingId)
        {
            var rating = _unitOfWork.RatingRepository.GetByID(ratingId);
            return await Task.FromResult(rating);
        }

        public async Task<IEnumerable<Rating>> GetRatingsByRecipe(int recipeId)
        {
            try
            {
                var ratings = await _unitOfWork.RatingRepository
                    .GetAsync(r => r.Recipe.recipeId == recipeId);

                return ratings;
            }
            catch (Exception e)
            {
                Console.WriteLine("An errror occured while getting recipes");
                throw;
            }
        }

        public async Task Update(Rating rating)
        {
            try
            {
                _unitOfWork.RatingRepository.Update(rating);
                _unitOfWork.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred while updating rating");
                throw;
            }
        }

        public async Task Add(Rating rating)
        {
            try
            {
                var ratingRepos = _unitOfWork.RatingRepository;
                ratingRepos.Insert(rating);
                _unitOfWork.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine("An Error occured while adding rating");
                throw;
            }
        }
        public async Task Delete(int ratingId)
        {
            try
            {
                var ratingRepos = _unitOfWork.RatingRepository;
                ratingRepos.Delete(ratingId);
                _unitOfWork.Save();

            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured while deleting rating");
                throw;
            }
        }
    }
}
