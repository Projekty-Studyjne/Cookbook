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
        public async void TestRecipesByIngredientAction()
        {
            int ingredientId = 2;
            var recipes = new List<Recipe>() {
            new Recipe()
            {},
            new Recipe()
            {},
           };
        
            Mock<IRecipeService> mockRecipe = new Mock<IRecipeService>();
            mockRecipe
                .Setup(s => s.GetRecipesByIngredient(ingredientId))
                .ReturnsAsync(recipes);
            RecipesController recipesController = new RecipesController(mockRecipe.Object);
            var result = await recipesController.RecipesByIngredient(ingredientId);

            Assert.IsType<ViewResult>(result);

            var viewResult =(ViewResult)result;
            var model = Assert.IsAssignableFrom<IEnumerable<Recipe>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async void TestRecipesByCategoryAction()
        {
            int categoryId = 2;
            var recipes = new List<Recipe>() {
            new Recipe()
            {},
            new Recipe()
            {},
           };

            Mock<IRecipeService> mockRecipe = new Mock<IRecipeService>();
            mockRecipe
                .Setup(s => s.GetRecipesByCategory(categoryId))
                .ReturnsAsync(recipes);
            RecipesController recipesController = new RecipesController(mockRecipe.Object);
            var result = await recipesController.RecipeByCategory(categoryId);
            Assert.IsType<ViewResult>(result);
            var viewResult = (ViewResult)result;
            var model = Assert.IsAssignableFrom<IEnumerable<Recipe>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }


    }
}
