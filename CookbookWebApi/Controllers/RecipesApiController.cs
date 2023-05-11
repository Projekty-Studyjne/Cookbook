using CookbookBLL.Interfaces;
using CookbookLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace CookbookWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecipesApiController : Controller
    {
        private readonly IRecipeService _recipeService;
        public RecipesApiController(IRecipeService recipeService)
        {
            this._recipeService = recipeService;
        }
        [HttpGet]
        public IEnumerable<Recipe> GetAll()
        {
            IEnumerable<Recipe> recipes = new List<Recipe>();
            recipes = _recipeService.GetAll().Result;
            return recipes;
        }
        //public IActionResult GetAll()
        //{
        //    IEnumerable<Recipe> recipes = _recipeService.GetAll().Result;
        //    string jsonString = JsonSerializer.Serialize(recipes, _jsonSerializerOptions);
        //    return new JsonResult(jsonString);
        //}
    }
}
