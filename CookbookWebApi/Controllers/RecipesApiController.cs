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

        [HttpGet("{id}")]
        public Recipe GetOne(int id)
        {
            Recipe recipe = null;
            recipe = _recipeService.GetRecipeById(id).Result;
            return recipe;
        }

        [HttpGet("/RecipesApi/ByIngredient/{id}")]
        public IEnumerable<Recipe> GetRecipeByIngredient(int id)
        {
            IEnumerable<Recipe> recipes = new List<Recipe>();
            recipes = _recipeService.GetRecipesByIngredient(id).Result;
            return recipes;
        }

        [HttpGet("/RecipesApi/ByCategory/{id}")]
        public IEnumerable<Recipe> GetRecipeByCategory(int id)
        {
            IEnumerable<Recipe> recipes = new List<Recipe>();
            recipes = _recipeService.GetRecipesByCategory(id).Result;
            return recipes;
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            if (_recipeService.Delete(id).IsCompletedSuccessfully)
                return true;
            return false;
        }

        [HttpPost]
        public bool Post(Recipe recipe)
        {
            if (_recipeService.Add(recipe).IsCompletedSuccessfully)
            {
                return true;
            }
            return false;
        }

        [HttpPost("{id}")]
        public bool AddCategoryToRecipe(int id,[FromBody] Category category)
        {
            if (_recipeService.AddCategoryToRecipe(id,category).IsCompletedSuccessfully)
            {
                return true;
            }
            return false;
        }

        [HttpPut]
        public bool Put(Recipe recipe)
        {
            if (_recipeService.Update(recipe).IsCompletedSuccessfully)
                return true;
            return false;
        }

    }
}
