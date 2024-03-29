﻿using CookbookBLL;
using CookbookBLL.Interfaces;
using CookbookLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
        public IEnumerable<IngredientResponse> GetAll()
        {
            //IEnumerable<Ingredient> ingredients = new List<Ingredient>();
            //ingredients = _ingredientService.GetAll().Result;
            //return ingredients;
            return _ingredientService.GetAll().Result.Select(x => new IngredientResponse(x.ingredientId, x.name, x.category,null, null));
        }

        [HttpGet("{id}")]
        public IngredientResponse GetOne(int id)
        {
            Ingredient ingredient = null;
            ingredient = _ingredientService.GetIngredientById(id).Result;
            if (ingredient != null)
            {
                return new IngredientResponse(ingredient.ingredientId, ingredient.name, ingredient.category,null, null);
            }
            return null;
        }

        [HttpGet("/IngredientApi/ByRecipe/{id}")]
        public IEnumerable<IngredientResponse> GetIngredientByRecipe(int id)
        {
            return _ingredientService.GetIngredientsByRecipe(id).Result.Select(x => new IngredientResponse(x.ingredientId,x.name,x.category,x.IngredientRecipes.Single(a=>a.ingredientId==x.ingredientId && a.recipeId==id).quantity,x.IngredientRecipes.Single(a => a.ingredientId == x.ingredientId && a.recipeId == id).unit));
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            if (_ingredientService.Delete(id).IsCompletedSuccessfully)
                return true;
            return false;
        }

        [HttpPost]
        public bool Post([FromBody]IngredientRequest ingredientReq)
        {
            Ingredient ingredient = new Ingredient();
            ingredient.name = ingredientReq.name;
            ingredient.category = ingredientReq.category;
            if (_ingredientService.Add(ingredient).IsCompletedSuccessfully)
            {
                return true;
            }
            return false;
        }

        [HttpPut]
        public bool Put(int id,[FromBody]IngredientRequest ingredientReq)
        {
            Ingredient? ingredient = _ingredientService.GetIngredientById(id).Result;
            if (ingredient != null)
            {
                ingredient.name = ingredientReq.name;
                ingredient.category = ingredientReq.category;
                if (_ingredientService.Update(ingredient).IsCompletedSuccessfully)
                    return true;
            }
            return false;
        }
    }
}
