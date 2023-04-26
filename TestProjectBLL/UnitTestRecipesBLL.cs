using Moq;
using CookbookBLL;
using CookbookLibrary;
using CookbookLibrary.Entities;
using CookbookLibrary.Repositories;
using CookbookLibrary.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

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
        //Z biblioteką Moq
        [Fact]
        public void TestGetRecipesByIngredientMoq()
        {
            var recipes = new List<Recipe>
            {
            new Recipe { recipeId = 1, title = "Dumplings", description = "Recipe for dumplings", IngredientRecipes = new List<IngredientRecipe> { new IngredientRecipe { ingredientId = 2 } }  },
            new Recipe { recipeId = 2, title = "Pizza", description = "Recipe for pizza", IngredientRecipes = new List<IngredientRecipe> { new IngredientRecipe { ingredientId = 1 } } },
            new Recipe { recipeId = 3, title = "Omelette", description = "Recipe for omelette", IngredientRecipes = new List<IngredientRecipe> { new IngredientRecipe { ingredientId = 2 } } }
            };

            Mock<IGenericRepository<Recipe>> mockRepo = new Mock<IGenericRepository<Recipe>>();
            mockRepo.Setup(m => m.GetAsync(
            It.IsAny<Expression<Func<Recipe, bool>>>(),
            It.IsAny<Func<IQueryable<Recipe>, IOrderedQueryable<Recipe>>>(),
            It.IsAny<string>()))
            .ReturnsAsync((Expression<Func<Recipe, bool>> filter,
                   Func<IQueryable<Recipe>, IOrderedQueryable<Recipe>> orderBy,
                   string includeProperties) =>
            {
                return recipes.Where(filter.Compile());
            });

            var unitOfWork = new TestUnitOfWork(mockRepo.Object);
            var recipeService = new RecipeService(unitOfWork);
            var result = recipeService.GetRecipesByIngredient(2).Result;

            Assert.Equal(2, result.Count());
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
