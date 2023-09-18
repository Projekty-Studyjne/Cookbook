using CookbookBLL;
using CookbookBLL.Interfaces;
using CookbookLibrary.Entities;
using CookbookMVCBLL.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestControlersMVC
{
    public class UnitTestRecipeController
    {
        [Fact]
        public async void TestIndex()
        {
            var mockService = new Mock<IRecipeService>();
            var recipesController = new RecipesController(mockService.Object);
            var recipes = new List<Recipe>()
            {
                new Recipe() {
                recipeId = 1,
                title = "Test Recipe" 
                },
                new Recipe() {
                recipeId = 2,
                title = "Test Recipe"
                }
            };
            mockService.Setup(service => service.GetAll()).ReturnsAsync(recipes);
            var result = await recipesController.Index();
            var viewResult = (ViewResult)result;
            var model = Assert.IsAssignableFrom<IEnumerable<Recipe>>(viewResult.ViewData.Model);
            Assert.IsType<ViewResult>(result);
            Assert.Equal(recipes, model);
        }
        [Fact]
        public async void TestCreatePost()
        {
            var mockService = new Mock<IRecipeService>();
            var recipesController = new RecipesController(mockService.Object);
            var recipe = new Recipe{ recipeId = 1, title = "Test Recipe" };
            var result = await recipesController.Create(recipe);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("SelectIngredients", redirectToActionResult.ActionName);
        }

        [Fact]
        public async void TestEditPost()
        {
            var mockService = new Mock<IRecipeService>();
            var recipesController = new RecipesController(mockService.Object);
            var recipe = new Recipe { recipeId = 1, title = "Test Recipe" };
            mockService.Setup(service => service.Update(It.IsAny<Recipe>())).Verifiable();
            var result = await recipesController.Edit(1, recipe);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockService.Verify();
        }

        [Fact]
        public async void TestDeleteConfirmed()
        {
            var mockService = new Mock<IRecipeService>();
            var recipesController = new RecipesController(mockService.Object);
            var recipeId = 1;
            mockService.Setup(service => service.Delete(recipeId)).Verifiable();
            var result = await recipesController.DeleteConfirmed(recipeId);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockService.Verify();
        }
        [Fact]
        public async void TestDelete()
        {
            var mockService = new Mock<IRecipeService>();
            var recipesController = new RecipesController(mockService.Object);
            var recipeId = 1;
            var recipe = new Recipe { recipeId = 1, title = "Test Recipe" };
            mockService.Setup(service => service.GetRecipeById(recipeId)).ReturnsAsync(recipe);
            var result = await recipesController.Delete(recipeId);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Recipe>(viewResult.ViewData.Model);
            Assert.Equal(recipe, model);
        }

        [Fact]
        public async void TestRecipesByIngredient()
        {
            var mockService = new Mock<IRecipeService>();
            var recipesController = new RecipesController(mockService.Object);
            var ingredientName = "Pizza";
            var recipes = new List<Recipe>();
            mockService.Setup(service => service.GetRecipesByIngredientName(ingredientName)).ReturnsAsync(recipes);
            var result = await recipesController.RecipesByIngredientName(ingredientName);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Recipe>>(viewResult.ViewData.Model);
            Assert.Equal(recipes, model);
        }

        [Fact]
        public async void TestRecipeByCategory()
        {
            var mockService = new Mock<IRecipeService>();
            var recipesController = new RecipesController(mockService.Object);
            var categoryId = 1;
            var recipes = new List<Recipe>()
            {
                new Recipe() {
                recipeId = 1,
                title = "Test Recipe"
                },
                new Recipe() {
                recipeId = 2,
                title = "Test Recipe"
                }
            };
            mockService.Setup(service => service.GetRecipesByCategory(categoryId)).ReturnsAsync(recipes);
            var result = await recipesController.RecipeByCategory(categoryId);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Recipe>>(viewResult.ViewData.Model);
            Assert.Equal(recipes, model);
        }

        [Fact]
        public async void TestAddCategoryToRecipe()
        {
            var mockService = new Mock<IRecipeService>();
            var recipesController = new RecipesController(mockService.Object);
            var recipeId = 1;
            var categoryId = 1;
            mockService.Setup(service => service.AddCategoryToRecipe(recipeId, categoryId)).Verifiable();
            var result = await recipesController.AddCategoryToRecipe(recipeId, categoryId) as RedirectResult;
            Assert.NotNull(result);
            Assert.Equal("http://localhost:4200/LogInUser/" + UserService.getUserId(), result.Url);
            mockService.Verify();
        }
    }
}
