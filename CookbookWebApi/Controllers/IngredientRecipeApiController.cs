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
    public class IngredientRecipeApiController : Controller
    {
        private readonly IIngredientRecipeService _ingredientRecipeService;
        public IngredientRecipeApiController(IIngredientRecipeService ingredientRecipeService)
        {
            this._ingredientRecipeService = ingredientRecipeService;
        }
        //[HttpGet]
        //public IEnumerable<RecipeResponse> GetAll()
        //{
        //    //IEnumerable<Recipe> recipes = new List<Recipe>();
        //    //recipes = _recipeService.GetAll().Result;
        //    //return recipes;
        //    return _recipeService.GetAll().Result.Select(x => new RecipeResponse(x.recipeId, x.title, x.description, x.instructions, x.preparation_time, x.servings, x.IngredientRecipes));
        //}

        [HttpGet("{id}")]
        public IngredientRecipe GetOne(int id)
        {
            return _ingredientRecipeService.GetIngredientsByRecipe(id).Result;
        }

        //[HttpGet("/RecipesApi/ByIngredient/{id}")]
        //public IEnumerable<RecipeResponse> GetRecipeByIngredient(int id)
        //{
        //    //IEnumerable<Recipe> recipes = new List<Recipe>();
        //    //recipes = _recipeService.GetRecipesByIngredient(id).Result;
        //    //return recipes;
        //    return _recipeService.GetRecipesByIngredient(id).Result.Select(x => new RecipeResponse(x.recipeId, x.title, x.description, x.instructions, x.preparation_time, x.servings, x.IngredientRecipes));
        //}

        //[HttpGet("/RecipesApi/ByCategory/{id}")]
        //public IEnumerable<RecipeResponse> GetRecipeByCategory(int id)
        //{
        //    //IEnumerable<Recipe> recipes = new List<Recipe>();
        //    //recipes = _recipeService.GetRecipesByCategory(id).Result;
        //    //return recipes;
        //    return _recipeService.GetRecipesByCategory(id).Result.Select(x => new RecipeResponse(x.recipeId, x.title, x.description, x.instructions, x.preparation_time, x.servings, x.IngredientRecipes));
        //}

        //[HttpDelete("{id}")]
        //public bool Delete(int id)
        //{
        //    if (_recipeService.Delete(id).IsCompletedSuccessfully)
        //        return true;
        //    return false;
        //}

        //[HttpPost]
        //public bool Post([FromBody] RecipeRequest recipeReq)
        //{
        //    Recipe recipe = new Recipe();
        //    recipe.title = recipeReq.title;
        //    recipe.description = recipeReq.description;
        //    recipe.instructions = recipeReq.instructions;
        //    recipe.preparation_time = recipeReq.preparation_time;
        //    recipe.servings = recipeReq.servings;
        //    if (_recipeService.Add(recipe).IsCompletedSuccessfully)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //[HttpPost("{id}")]
        //public bool AddCategoryToRecipe(int id, [FromBody] CategoryRequest categoryReq)
        //{
        //    Category category = new Category();
        //    category.name = categoryReq.name;
        //    category.description = categoryReq.description;
        //    if (_recipeService.AddCategoryToRecipe(id, category).IsCompletedSuccessfully)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //[HttpPut]
        //public bool Put(int id, [FromBody] RecipeRequest recipeReq)
        //{
        //    Recipe recipe = _recipeService.GetRecipeById(id).Result;
        //    if (recipe != null)
        //    {
        //        recipe.title = recipeReq.title;
        //        recipe.description = recipeReq.description;
        //        recipe.servings = recipeReq.servings;
        //        recipe.preparation_time = recipeReq.preparation_time;
        //        recipe.instructions = recipeReq.instructions;
        //        if (_recipeService.Update(recipe).IsCompletedSuccessfully)
        //            return true;
        //    }
        //    return false;
        //}



    }
}
