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
        public IEnumerable<Rating> GetAll()
        {
            IEnumerable<Rating> ratings = new List<Rating>();
            ratings = _ratingService.GetAll().Result;
            return ratings;
        }

        [HttpGet("{id}")]
        public Rating GetOne(int id)
        {
            Rating rating = null;
            rating = _ratingService.GetRatingById(id).Result;
            return rating;
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            if (_ratingService.Delete(id).IsCompletedSuccessfully)
                return true;
            return false;
        }

        [HttpPost]
        public bool Post(Rating rating)
        {
            if (_ratingService.Add(rating).IsCompletedSuccessfully)
            {
                return true;
            }
            return false;
        }

        [HttpPut]
        public bool Put(Rating rating)
        {
            if (_ratingService.Update(rating).IsCompletedSuccessfully)
                return true;
            return false;
        }
    }
}
