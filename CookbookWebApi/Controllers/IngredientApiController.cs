using CookbookBLL.Interfaces;
using CookbookLibrary.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CookbookWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IngredientApiController : Controller
    {
        
        private readonly IIngredientService _ingredientService;
        public IngredientApiController(IIngredientService ingredientService)
        {
            this._ingredientService = ingredientService;
        }
        [HttpGet]
        public IEnumerable<Ingredient> GetAll()
        {
            IEnumerable<Ingredient> ingredients = new List<Ingredient>();
            ingredients = _ingredientService.GetAll().Result;
            return ingredients;
        }

        [HttpGet("{id}")]
        public Ingredient GetOne(int id)
        {
            Ingredient ingredient = null;
            ingredient = _ingredientService.GetIngredientById(id).Result;
            return ingredient;
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            if (_ingredientService.Delete(id).IsCompletedSuccessfully)
                return true;
            return false;
        }

        [HttpPost]
        public bool Post(Ingredient ingredient)
        {
            if (_ingredientService.Add(ingredient).IsCompletedSuccessfully)
            {
                return true;
            }
            return false;
        }

        [HttpPut]
        public bool Put(Ingredient ingredient)
        {
            if (_ingredientService.Update(ingredient).IsCompletedSuccessfully)
                return true;
            return false;
        }
    }
}
