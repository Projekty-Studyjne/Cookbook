using CookbookBLL;
using CookbookLibrary.Entities;
using CookbookLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectBLL
{
    public class UnitTestRecipesBLL
    {
        [Fact]
        public void  TestGetRecipesByIngredient()
        {
            var recipeRepo = new RecipeRepoFake();
            recipeRepo.InsertRecipe(new Recipe { recipeId = 1, title = "Pierogi", description = "Przepis na pierogi", IngredientRecipes = new List<IngredientRecipe> { new IngredientRecipe { ingredientId = 2 } } });
            recipeRepo.InsertRecipe(new Recipe { recipeId = 2, title = "Pizza", description = "Przepis na pizzę", IngredientRecipes = new List<IngredientRecipe> { new IngredientRecipe { ingredientId = 1 } } });
            recipeRepo.InsertRecipe(new Recipe { recipeId = 3, title = "Omlet", description = "Przepis na omlet", IngredientRecipes = new List<IngredientRecipe> { new IngredientRecipe { ingredientId = 2 } } });

            // Tworzenie jednostki pracy i usługi przepisu
            var unitOfWork = new UnitOfWork();
            var recipeService = new RecipeService(unitOfWork);

            // Wywołanie metody GetRecipesByIngredient() z identyfikatorem składnika równym 2
            var result = recipeService.GetRecipesByIngredient(2).Result;

            // Sprawdzenie, czy wynik zawiera dwa przepisy (Pierogi i Omlet)
            Console.WriteLine(result.Count());
            Assert.Contains(result, r => r.recipeId == 1);
            Assert.Contains(result, r => r.recipeId == 3);
        }

        //public async Task<IEnumerable<Recipe>> GetRecipesByCategory(int categoryId)
        //{
        //    try
        //    {
        //        var recipes = await _unitOfWork.RecipeRepository
        //            .GetAsync(r => r.CategoryRecipes.Any(i => i.categoryId == categoryId),
        //                includeProperties: "Categories");

        //        return recipes;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("An errror occured while getting recipes");
        //        throw;
        //    }
        //}

        //public async Task AddCategoryToRecipe(int recipeId, Category category)
        //{

        //    var recipes = await _unitOfWork.RecipeRepository.GetAsync(r => r.recipeId == recipeId,
        //                                                  includeProperties: "CategoryRecipes");
        //    var recipe = recipes.SingleOrDefault();

        //    if (recipe == null)
        //    {
        //        throw new Exception("Recipe not found");
        //    }

        //    var existingCategoryRecipe = recipe.CategoryRecipes
        //                                       .SingleOrDefault(cr => cr.categoryId == category.categoryId);
        //    if (existingCategoryRecipe != null)
        //    {
        //        throw new Exception("Category already assigned to recipe");
        //    }

        //    var categoryRecipe = new CategoryRecipe { Recipe = recipe, Category = category };
        //    _unitOfWork.CategoryRecipeRepository.Insert(categoryRecipe);
        //    await _unitOfWork.SaveAsync();
        //}
    }
}
