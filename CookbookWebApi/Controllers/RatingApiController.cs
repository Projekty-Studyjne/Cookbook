using CookbookBLL;
using CookbookBLL.Interfaces;
using CookbookLibrary.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CookbookWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RatingApiController : Controller
    {
        private readonly IRatingService _ratingService;
        public RatingApiController(IRatingService ratingService)
        {
            this._ratingService = ratingService;
        }
        [HttpGet]
        public IEnumerable<RatingResponse> GetAll()
        {
            //IEnumerable<Rating> ratings = new List<Rating>();
            //ratings = _ratingService.GetAll().Result;
            //return ratings;
            return _ratingService.GetAll().Result.Select(x => new RatingResponse(x.ratingId, x.rating, x.userId, x.recipeId));
        }

        [HttpGet("{id}")]
        public RatingResponse GetOne(int id)
        {
            Rating rating = null;
            rating = _ratingService.GetRatingById(id).Result;
            if(rating != null)
            {
                return new RatingResponse(rating.ratingId, rating.rating, rating.userId, rating.recipeId);
            }
            return null;
        }

        [HttpGet("/RatingsApi/ByRecipe/{recipeId}")]
        public IEnumerable<RatingResponse> GetRatingsByRecipe(int recipeId)
        {
            return _ratingService.GetRatingsByRecipe(recipeId).Result.Select(x => new RatingResponse(x.ratingId, x.rating, x.userId, x.recipeId));
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            if (_ratingService.Delete(id).IsCompletedSuccessfully)
                return true;
            return false;
        }

        [HttpPost]
        public bool Post([FromBody]RatingRequest ratingReq)
        {
            Rating rating = new Rating();
            rating.rating = ratingReq.rating;
            rating.userId = ratingReq.userId;
            rating.recipeId= ratingReq.recipeId;
            if (_ratingService.Add(rating).IsCompletedSuccessfully)
            {
                return true;
            }
            return false;
        }

        [HttpPut]
        public bool Put(int id,[FromBody]RatingRequest ratingReq)
        {
            Rating? rating = _ratingService.GetRatingById(id).Result;
            if (rating != null)
            {
                rating.rating = ratingReq.rating;
                rating.userId = ratingReq.userId;
                rating.recipeId = ratingReq.recipeId;
                if (_ratingService.Update(rating).IsCompletedSuccessfully)
                    return true;
            }
            return false;
        }
    }
}
