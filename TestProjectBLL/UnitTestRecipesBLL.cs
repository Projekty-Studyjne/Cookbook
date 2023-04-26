using CookbookBLL;
using CookbookLibrary;
using CookbookLibrary.Entities;
using CookbookLibrary.Repositories;
using CookbookLibrary.RepositoryInterfaces;
using Moq;
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
        public void TestGetRecipesByIngredient()
        {
            CookbookDbContext context = new CookbookDbContext();
            var recipeRepo = new RecipeRepoFake();
            recipeRepo.Insert(new Recipe { recipeId = 1, title = "Dumplings", description = "Recipe for dumplings", IngredientRecipes = new List<IngredientRecipe> { new IngredientRecipe { ingredientId = 2 } } });
            recipeRepo.Insert(new Recipe { recipeId = 2, title = "Pizza", description = "Recipe for pizza", IngredientRecipes = new List<IngredientRecipe> { new IngredientRecipe { ingredientId = 1 } } });
            recipeRepo.Insert(new Recipe { recipeId = 3, title = "Omelette", description = "Recipe for omelette", IngredientRecipes = new List<IngredientRecipe> { new IngredientRecipe { ingredientId = 2 } } });

            // Creating unit of work and recipe service
            var unitOfWork = new TestUnitOfWork(recipeRepo);
            var recipeService = new RecipeService(unitOfWork);

            // Calling the GetRecipesByIngredient() method with the ingredient identifier equal to 2
            var result = recipeService.GetRecipesByIngredient(2).Result;
            Console.WriteLine(result);
            // Checking if the result contains two recipes (Dumplings and Omelette)
            //Assert.Equal(2, result.Count());
            //Console.WriteLine(result.Count());
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
