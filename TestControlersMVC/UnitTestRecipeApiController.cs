using CookbookBLL.Interfaces;
using CookbookLibrary.Entities;
using CookbookWebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestControlersMVC
{
    public class UnitTestRecipeApiController
    {
        //[Fact]
        //public async void TestRecipesByIngredientAction()
        //{
        //    int ingredientId = 3;
        //    var recipes = new List<Recipe>() {
        //    new Recipe(){},
        //    new Recipe(){},
        //   };

        //    Mock<IRecipeService> mockRecipe = new Mock<IRecipeService>();

        //    mockRecipe
        //        .Setup(s => s.GetRecipesByIngredient(ingredientId))
        //        .ReturnsAsync(recipes);
        //    RecipesApiController recipesApiController = new RecipesApiController(mockRecipe.Object);

        //    Assert.Equal(2, recipesApiController.GetRecipeByIngredient(ingredientId).Count());
        //}

        [Fact]
        public async void TestRecipesByCategoryAction()
        {
            int categoryId = 2;
            var recipes = new List<Recipe>() {
            new Recipe(){},
            new Recipe(){},
           };

            Mock<IRecipeService> mockRecipe = new Mock<IRecipeService>();

            mockRecipe
                .Setup(s => s.GetRecipesByCategory(categoryId))
                .ReturnsAsync(recipes);
            RecipesApiController recipesApiController = new RecipesApiController(mockRecipe.Object);

            Assert.Equal(2, recipesApiController.GetRecipeByCategory(categoryId).Count());
        }
    }
}
