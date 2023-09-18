using CookbookLibrary;
using CookbookLibrary.Entities;
using CookbookLibrary.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectDAL
{
    public class UnitTestRecipesRepo
    {
        //[Fact]
        //public async void TestGetRecipes()
        //{
        //    var options = new DbContextOptionsBuilder<CookbookDbContext>()
        //        .UseInMemoryDatabase("CookbookTest")
        //        .Options;

        //    var cookbookContext = new CookbookDbContext(options);
        //    GenericRepository<Recipe> recipeRepo = new GenericRepository<Recipe>(cookbookContext);
        //    Assert.Empty(await recipeRepo.GetAsync());
        //    recipeRepo.Insert(new Recipe {
        //        recipeId = 3,
        //        title = "Chocolate Chip Cookies",
        //        description = "A classic American dessert",
        //        instructions = "1. Preheat oven to 375°F. 2. Cream together butter, white sugar, and brown sugar until smooth. 3. Beat in eggs one at a time, then stir in vanilla. 4. Dissolve baking soda in hot water and add to batter. 5. Stir in flour, chocolate chips, and nuts. 6. Drop by large spoonfuls onto ungreased pans. 7. Bake for about 10 minutes or until edges are nicely browned.",
        //        preparation_time = 30,
        //        servings = 24,
        //        IngredientRecipes = new List<IngredientRecipe> { new IngredientRecipe { ingredientId = 1, recipeId = 1, quantity = 4, unit = "large" } } });
        //    cookbookContext.SaveChanges();
        //    Assert.Equal(1, recipeRepo.GetAsync().Result.Count());
        //}
    }
}
