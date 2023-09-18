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
        public void TestGetAll()
        {
            var recipeRepository = new RecipeRepoFake();
            var unitOfWork = new TestUnitOfWork(recipeRepository);
            var recipeService = new RecipeService(unitOfWork);
            var recipes = recipeService.GetAll();
            Assert.NotNull(recipes);
        }

        [Fact]
        public void TestGetRecipeById()
        {
            var recipeRepository = new RecipeRepoFake();
            var unitOfWork = new TestUnitOfWork(recipeRepository);
            var recipeService = new RecipeService(unitOfWork);
            var recipe = new Recipe
            {
                recipeId = 1,
                title = "Test Recipe"
            };
            recipeRepository.Insert(recipe);
            var retrievedRecipe = recipeService.GetRecipeById(recipe.recipeId).Result;
            Assert.NotNull(retrievedRecipe);
            Assert.Equal(recipe.recipeId, retrievedRecipe.recipeId);
        }

        [Fact]
        public void TestUpdate()
        {
            var recipeRepository = new RecipeRepoFake();
            var unitOfWork = new TestUnitOfWork(recipeRepository);
            var recipeService = new RecipeService(unitOfWork);
            var recipe = new Recipe
            {
                recipeId = 1,
                title = "Test Recipe"
            };
            recipeRepository.Insert(recipe);
            recipe.title = "Updated Recipe Title";
            recipeService.Update(recipe);
            var updatedRecipe = recipeRepository.GetByID(recipe.recipeId);
            Assert.NotNull(updatedRecipe);
            Assert.Equal(recipe.title, updatedRecipe.title);
        }

        [Fact]
        public void TestAdd()
        {
            var recipeRepository = new RecipeRepoFake();
            var unitOfWork = new TestUnitOfWork(recipeRepository);
            var recipeService = new RecipeService(unitOfWork);
            var recipe = new Recipe
            {
                recipeId = 1,
                title = "Test Recipe"
            };
            recipeService.Add(recipe);
            var addedRecipe = recipeRepository.GetByID(recipe.recipeId);
            Assert.NotNull(addedRecipe);
            Assert.Equal(recipe.recipeId, addedRecipe.recipeId);
        }

        [Fact]
        public void TestDelete()
        {
            var recipeRepository = new RecipeRepoFake();
            var unitOfWork = new TestUnitOfWork(recipeRepository);
            var recipeService = new RecipeService(unitOfWork);
            var recipe = new Recipe
            {
                recipeId = 1,
                title = "Test Recipe"
            };
            recipeRepository.Insert(recipe);
            recipeService.Delete(recipe.recipeId);
            var deletedRecipe = recipeRepository.GetByID(recipe.recipeId);
            Assert.Null(deletedRecipe);
        }
        [Fact]
        public void TestGetRecipesByIngredient()
        {
            CookbookDbContext context = new CookbookDbContext();
            var recipeRepo = new RecipeRepoFake();
            var ingredientRepo = new IngredientRepoFake();
            var recipe = new Recipe { recipeId = 1, title = "Dumplings", description = "Recipe for dumplings" };
            var ingredient = new Ingredient { ingredientId = 1, name = "Eggs", category = "Protein" };
            var ingedientRecipe = new IngredientRecipe { Recipe = recipe, Ingredient = ingredient };
            recipe.IngredientRecipes = new List<IngredientRecipe> { ingedientRecipe };
            var unitOfWork = new TestUnitOfWork(recipeRepo, ingredientRepo);
            ingredientRepo.Insert(ingredient);
            recipeRepo.Insert(recipe);
            var recipeService = new RecipeService(unitOfWork);
            var result = recipeService.GetRecipesByIngredientName("Eggs").Result;

            Assert.Single(result);
        }
        
        [Fact]
        public void TestGetRecipesByName()
        {
            CookbookDbContext context = new CookbookDbContext();
            var recipeRepo = new RecipeRepoFake();
            recipeRepo.Insert(new Recipe { recipeId = 1, title = "Dumplings", description = "Recipe for dumplings" });
            recipeRepo.Insert(new Recipe { recipeId = 2, title = "Pizza", description = "Recipe for pizza" });
            recipeRepo.Insert(new Recipe { recipeId = 3, title = "Omelette", description = "Recipe for omelette" });

            var unitOfWork = new TestUnitOfWork(recipeRepo);
            var recipeService = new RecipeService(unitOfWork);
            var result = recipeService.GetRecipesByName("Pizza").Result;

            Assert.Contains(result, r => r.recipeId == 2);
        }

        [Fact]
        public void TestGetRecipesByCategory()
        {
            CookbookDbContext context = new CookbookDbContext();
            var recipeRepo = new RecipeRepoFake();
            recipeRepo.Insert(new Recipe { recipeId = 1, title = "Dumplings", description = "Recipe for dumplings", CategoryRecipes = new List<CategoryRecipe> { new CategoryRecipe { categoryId = 2, recipeId = 1 } } });
            recipeRepo.Insert(new Recipe { recipeId = 2, title = "Pizza", description = "Recipe for pizza", CategoryRecipes = new List<CategoryRecipe> { new CategoryRecipe { categoryId = 1, recipeId = 2 } } });
            recipeRepo.Insert(new Recipe { recipeId = 3, title = "Omelette", description = "Recipe for omelette", CategoryRecipes = new List<CategoryRecipe> { new CategoryRecipe { categoryId = 1, recipeId = 3 } } });

            var unitOfWork = new TestUnitOfWork(recipeRepo);
            var recipeService = new RecipeService(unitOfWork);
            var result = recipeService.GetRecipesByCategory(2).Result;

            Assert.Contains(result, r => r.recipeId == 1);
        }

        [Fact]
        public void TestAddCategoryToRecipe()
        {
            {
                var recipeRepository = new RecipeRepoFake();
                var categoryRecipeRepository = new CategoryRecipeRepoFake();
                var unitOfWork = new TestUnitOfWork(recipeRepository, categoryRecipeRepository);
                var recipeService = new RecipeService(unitOfWork);
                var recipe = new Recipe
                {
                    recipeId = 1,
                    title = "Test Recipe"
                };

                var category = new Category
                {
                    categoryId = 1,
                    name = "Test Category",
                    description = "Test Description"
                };
                recipeRepository.Insert(recipe);
                recipeService.AddCategoryToRecipe(recipe.recipeId, category.categoryId);
                var updatedRecipe = (recipeRepository.GetAsync(r => r.recipeId == recipe.recipeId)).Result;
                Assert.NotNull(updatedRecipe);
            }
        }

    }
}
