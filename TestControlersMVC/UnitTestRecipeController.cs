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
            Assert.Equal("Index", redirectToActionResult.ActionName);
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
            var category = new Category { categoryId = 1, name = "Breakfast", description = "Delicious breakfast recipes" };
            mockService.Setup(service => service.AddCategoryToRecipe(recipeId, category)).Verifiable();
            var result = await recipesController.AddCategoryToRecipe(recipeId, category);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("", viewResult.ViewName); 
            mockService.Verify();
        }

        [Fact]
        public void TestNewRecipe()
        {
            var mockService = new Mock<IRecipeService>();
            var recipesController = new RecipesController(mockService.Object);
            var result = recipesController.NewRecipe();
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("", viewResult.ViewName);
        }
        //[Fact]
        //public async void TestRecipesByIngredientAction()
        //{
        //    int ingredientId = 2;
        //    var recipes = new List<Recipe>() {
        //    new Recipe()
        //    {},
        //    new Recipe()
        //    {},
        //   };

        //    Mock<IRecipeService> mockRecipe = new Mock<IRecipeService>();
        //    mockRecipe
        //        .Setup(s => s.GetRecipesByIngredient(ingredientId))
        //        .ReturnsAsync(recipes);
        //    RecipesController recipesController = new RecipesController(mockRecipe.Object);
        //    var result = await recipesController.RecipesByIngredient(ingredientId);

        //    Assert.IsType<ViewResult>(result);

        //    var viewResult =(ViewResult)result;
        //    var model = Assert.IsAssignableFrom<IEnumerable<Recipe>>(viewResult.ViewData.Model);
        //    Assert.Equal(2, model.Count());
        //}

        //[Fact]
        //public async void TestRecipesByCategoryAction()
        //{
        //    int categoryId = 2;
        //    var recipes = new List<Recipe>() {
        //    new Recipe()
        //    {},
        //    new Recipe()
        //    {},
        //   };

        //    Mock<IRecipeService> mockRecipe = new Mock<IRecipeService>();
        //    mockRecipe
        //        .Setup(s => s.GetRecipesByCategory(categoryId))
        //        .ReturnsAsync(recipes);
        //    RecipesController recipesController = new RecipesController(mockRecipe.Object);
        //    var result = await recipesController.RecipeByCategory(categoryId);
        //    Assert.IsType<ViewResult>(result);
        //    var viewResult = (ViewResult)result;
        //    var model = Assert.IsAssignableFrom<IEnumerable<Recipe>>(viewResult.ViewData.Model);
        //    Assert.Equal(2, model.Count());
        //}


    }
}
