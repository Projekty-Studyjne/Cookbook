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

            var unitOfWork = new TestUnitOfWork(recipeRepo);
            var recipeService = new RecipeService(unitOfWork);
            var result = recipeService.GetRecipesByIngredient(2).Result;

            Console.WriteLine(result);
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
    }
}
