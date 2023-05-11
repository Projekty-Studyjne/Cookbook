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
            {
                recipeId = 1,
                title = "Chocolate Chip Cookies",
                description = "A classic American dessert",
                instructions = "1. Preheat oven to 375°F. 2. Cream together butter, white sugar, and brown sugar until smooth. 3. Beat in eggs one at a time, then stir in vanilla. 4. Dissolve baking soda in hot water and add to batter. 5. Stir in flour, chocolate chips, and nuts. 6. Drop by large spoonfuls onto ungreased pans. 7. Bake for about 10 minutes or until edges are nicely browned.",
                preparation_time = 30,
                servings = 24,
                IngredientRecipes = new List<IngredientRecipe> { new IngredientRecipe { ingredientId = 1, recipeId = 1, quantity = 4, unit = "large" } }
            },
            new Recipe()
            {
                recipeId = 2,
                title = "Chocolate Chip Cookies",
                description = "A classic American dessert",
                instructions = "1. Preheat oven to 375°F. 2. Cream together butter, white sugar, and brown sugar until smooth. 3. Beat in eggs one at a time, then stir in vanilla. 4. Dissolve baking soda in hot water and add to batter. 5. Stir in flour, chocolate chips, and nuts. 6. Drop by large spoonfuls onto ungreased pans. 7. Bake for about 10 minutes or until edges are nicely browned.",
                preparation_time = 30,
                servings = 24,
                IngredientRecipes = new List<IngredientRecipe> { new IngredientRecipe { ingredientId = 1, recipeId = 1, quantity = 4, unit = "large" } }
            },
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
    }
}
