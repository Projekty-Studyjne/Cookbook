using CookbookBLL;
using CookbookBLL.Interfaces;
using CookbookLibrary;
using CookbookLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectBLL
{
    public class UnitTestIngredientsBLL
    {
        [Fact]
        public void TestGetAll()
        {
            var ingredientRepository = new IngredientRepoFake();
            var unitOfWork = new TestUnitOfWork(ingredientRepository);
            var ingredientService = new IngredientService(unitOfWork);
            var ingredients = ingredientService.GetAll();
            Assert.NotNull(ingredients);
        }

        [Fact]
        public void TestGetIngredientById()
        {
            var ingredientRepository = new IngredientRepoFake();
            var unitOfWork = new TestUnitOfWork(ingredientRepository);
            var ingredientService = new IngredientService(unitOfWork);
            var ingredient = new Ingredient
            {
                ingredientId = 1,
                name = "Test Ingredient"
            };
            ingredientRepository.Insert(ingredient);
            var retrievedIngredient = ingredientService.GetIngredientById(ingredient.ingredientId).Result;
            Assert.NotNull(retrievedIngredient);
            Assert.Equal(ingredient.ingredientId, retrievedIngredient.ingredientId);
        }

        [Fact]
        public void TestUpdate()
        {
            var ingredientRepository = new IngredientRepoFake();
            var unitOfWork = new TestUnitOfWork(ingredientRepository);
            var ingredientService = new IngredientService(unitOfWork);
            var ingredient = new Ingredient
            {
                ingredientId = 1,
                name = "Test Ingredient"
            };
            ingredientRepository.Insert(ingredient);
            ingredient.name = "Updated Ingredient Name";
            ingredientService.Update(ingredient);
            var updatedIngredient = ingredientRepository.GetByID(ingredient.ingredientId);
            Assert.NotNull(updatedIngredient);
            Assert.Equal(ingredient.name, updatedIngredient.name);
        }

        [Fact]
        public void TestAdd()
        {
            var ingredientRepository = new IngredientRepoFake();
            var unitOfWork = new TestUnitOfWork(ingredientRepository);
            var ingredientService = new IngredientService(unitOfWork);
            var ingredient = new Ingredient
            {
                ingredientId = 1,
                name = "Test Ingredient"
            };
            ingredientService.Add(ingredient);
            var addedIngredient = ingredientRepository.GetByID(ingredient.ingredientId);
            Assert.NotNull(addedIngredient);
            Assert.Equal(ingredient.ingredientId, addedIngredient.ingredientId);
        }

        [Fact]
        public void TestDelete()
        {
            var ingredientRepository = new IngredientRepoFake();
            var unitOfWork = new TestUnitOfWork(ingredientRepository);
            var ingredientService = new IngredientService(unitOfWork);
            var ingredient = new Ingredient
            {
                ingredientId = 1,
                name = "Test Ingredient"
            };
            ingredientRepository.Insert(ingredient);
            ingredientService.Delete(ingredient.ingredientId);
            var deletedIngredient = ingredientRepository.GetByID(ingredient.ingredientId);
            Assert.Null(deletedIngredient);
        }

        [Fact]
        public void TestGetIngredientsByRecipe()
        {
            CookbookDbContext context = new CookbookDbContext();
            var ingredientRepo = new IngredientRepoFake();
            ingredientRepo.Insert(new Ingredient { ingredientId = 1, name = "Egg", category = "Protein", IngredientRecipes = new List<IngredientRecipe> { new IngredientRecipe { recipeId = 2, ingredientId = 1 } } });
            var unitOfWork = new TestUnitOfWork(ingredientRepo);
            var ingredientService = new IngredientService(unitOfWork);
            var result = ingredientService.GetIngredientsByRecipe(2).Result;
            Assert.Contains(result, r => r.ingredientId == 1);
        }
    }
}
