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
        [Fact]
        public async void TestRecipesByCategory()
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

        [Fact]
        public void TestGetAll()
        {
            var mockService = new Mock<IRecipeService>();
            mockService.Setup(service => service.GetAll()).ReturnsAsync(new List<Recipe>());
            var recipesController = new RecipesApiController(mockService.Object);
            var result = recipesController.GetAll();
            var recipes = Assert.IsAssignableFrom<IEnumerable<RecipeResponse>>(result);
            Assert.Empty(recipes);
        }

        [Fact]
        public void TestGetOneExistingRecipeReturnsRecipeResponse()
        {
            var recipeId = 1;
            var existingRecipe = new Recipe 
            {
                recipeId = 1,
                title = "Scrambled Eggs",
                description = "A classic breakfast dish",
                instructions = "1. Whisk eggs and milk together in a bowl. 2. Melt butter in a nonstick skillet over medium heat. 3. Pour egg mixture into skillet and cook, stirring occasionally, until eggs are set but still moist, about 3-5 minutes. 4. Season with salt and pepper to taste.",
                preparation_time = 10,
                servings = 2,
                imageUrl = "https://www.allrecipes.com/thmb/HcdHiuwiNOIlOISGKGTI0KxhR7E=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/JF_241160_CreamyCottageCheeseScrambled_4x3_12902-619d00dc88594ea9b8ed884a108db16d.jpg"
            };
            var mockService = new Mock<IRecipeService>();
            mockService.Setup(service => service.GetRecipeById(recipeId)).ReturnsAsync(existingRecipe);
            var recipesController = new RecipesApiController(mockService.Object);
            var result = recipesController.GetOne(recipeId);
            var recipeResponse = Assert.IsType<RecipeResponse>(result);
        }

        [Fact]
        public void TestGetOneNonExistingRecipeReturnsNull()
        {
            var recipeId = 999;
            var mockService = new Mock<IRecipeService>();
            mockService.Setup(service => service.GetRecipeById(recipeId)).ReturnsAsync((Recipe)null);
            var recipesController = new RecipesApiController(mockService.Object);
            var result = recipesController.GetOne(recipeId);
            Assert.Equal(result,null);
        }
        [Fact]
        public void TestDeleteExistingRecipeReturnsTrue()
        {
            var recipeId = 1;
            var mockService = new Mock<IRecipeService>();
            mockService.Setup(service => service.Delete(recipeId));
            var recipesController = new RecipesApiController(mockService.Object);
            var result = recipesController.Delete(recipeId);
            var deletionResult = Assert.IsType<bool>(result);
            Assert.True(deletionResult);
        }

        [Fact]
        public void TestDeleteNonExistingRecipeReturnsTrue()
        {
            var recipeId = 999;
            var mockService = new Mock<IRecipeService>();
            mockService.Setup(service => service.Delete(recipeId));
            var recipesController = new RecipesApiController(mockService.Object);
            var result = recipesController.Delete(recipeId);
            var deletionResult = Assert.IsType<bool>(result);
            Assert.True(deletionResult);
        }
        [Fact]
        public void TestPost()
        {
            var recipeRequest = new RecipeRequest
            {
                title = "Scrambled Eggs",
                description = "A classic breakfast dish",
                instructions = "1. Whisk eggs and milk together in a bowl. 2. Melt butter in a nonstick skillet over medium heat. 3. Pour egg mixture into skillet and cook, stirring occasionally, until eggs are set but still moist, about 3-5 minutes. 4. Season with salt and pepper to taste.",
                preparation_time = 10,
                servings = 2,
            };
            var mockService = new Mock<IRecipeService>();
            mockService.Setup(service => service.Add(It.IsAny<Recipe>()));
            var recipesController = new RecipesApiController(mockService.Object);
            var result = recipesController.Post(recipeRequest);
            Assert.Equal(result, true);
        }

        [Fact]
        public void TestAddCategoryToRecipe()
        {
            var recipeId = 1;
            var categoryRequest = new CategoryRequest
            {
                name = "Breakfast",
                description = "Delicious breakfast recipes"
            };
            var mockService = new Mock<IRecipeService>();
            mockService.Setup(service => service.AddCategoryToRecipe(recipeId, It.IsAny<int>()));
            var recipesController = new RecipesApiController(mockService.Object);
            var result = recipesController.AddCategoryToRecipe(recipeId, categoryRequest);
            var additionResult = Assert.IsType<bool>(result);
            Assert.True(additionResult);
        }

        [Fact]
        public void TestPutExistingRecipeReturnsTrue()
        {
            var recipeId = 1;
            var recipeRequest = new RecipeRequest
            {
                title = "Scrambled Eggs",
                description = "A classic breakfast dish",
                instructions = "1. Whisk eggs and milk together in a bowl. 2. Melt butter in a nonstick skillet over medium heat. 3. Pour egg mixture into skillet and cook, stirring occasionally, until eggs are set but still moist, about 3-5 minutes. 4. Season with salt and pepper to taste.",
                preparation_time = 10,
                servings = 2,
            };
            var existingRecipe = new Recipe
            {
                recipeId = 1,
                title = "Scrambled Eggs",
                description = "A classic breakfast dish",
                instructions = "1. Whisk eggs and milk together in a bowl. 2. Melt butter in a nonstick skillet over medium heat. 3. Pour egg mixture into skillet and cook, stirring occasionally, until eggs are set but still moist, about 3-5 minutes. 4. Season with salt and pepper to taste.",
                preparation_time = 10,
                servings = 2,
                imageUrl = "https://www.allrecipes.com/thmb/HcdHiuwiNOIlOISGKGTI0KxhR7E=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/JF_241160_CreamyCottageCheeseScrambled_4x3_12902-619d00dc88594ea9b8ed884a108db16d.jpg"
            };
            var mockService = new Mock<IRecipeService>();
            mockService.Setup(service => service.GetRecipeById(recipeId)).ReturnsAsync(existingRecipe);
            mockService.Setup(service => service.Update(existingRecipe));
            var recipesController = new RecipesApiController(mockService.Object);
            var result = recipesController.Put(recipeId, recipeRequest);
            Assert.Equal(result, true);
        }

        [Fact]
        public void TestPutNonExistingRecipeReturnsFalse()
        {
            var recipeId = 999;
            var recipeRequest = new RecipeRequest
            {
                title = "Scrambled Eggs",
                description = "A classic breakfast dish",
                instructions = "1. Whisk eggs and milk together in a bowl. 2. Melt butter in a nonstick skillet over medium heat. 3. Pour egg mixture into skillet and cook, stirring occasionally, until eggs are set but still moist, about 3-5 minutes. 4. Season with salt and pepper to taste.",
                preparation_time = 10,
                servings = 2,
            };
            var mockService = new Mock<IRecipeService>();
            mockService.Setup(service => service.GetRecipeById(recipeId)).ReturnsAsync((Recipe)null);
            var recipesController = new RecipesApiController(mockService.Object);
            var result = recipesController.Put(recipeId, recipeRequest);
            Assert.Equal(result,false);
        }

        [Fact]
        public void TestPut()
        {
            var recipeId = 2;
            var recipeRequest = new RecipeRequest
            {
                title = "Chocolate Chip Cookies",
                description = "A classic American dessert",
                instructions = "1. Preheat oven to 375°F. 2. Cream together butter, white sugar, and brown sugar until smooth. 3. Beat in eggs one at a time, then stir in vanilla. 4. Dissolve baking soda in hot water and add to batter. 5. Stir in flour, chocolate chips, and nuts. 6. Drop by large spoonfuls onto ungreased pans. 7. Bake for about 10 minutes or until edges are nicely browned.",
                preparation_time = 30,
                servings = 24,
            };
            var existingRecipe = new Recipe
            {
                recipeId = 2,
                title = "Scrambled Eggs",
                description = "A classic breakfast dish",
                instructions = "1. Whisk eggs and milk together in a bowl. 2. Melt butter in a nonstick skillet over medium heat. 3. Pour egg mixture into skillet and cook, stirring occasionally, until eggs are set but still moist, about 3-5 minutes. 4. Season with salt and pepper to taste.",
                preparation_time = 10,
                servings = 2,
                imageUrl = "https://www.allrecipes.com/thmb/HcdHiuwiNOIlOISGKGTI0KxhR7E=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/JF_241160_CreamyCottageCheeseScrambled_4x3_12902-619d00dc88594ea9b8ed884a108db16d.jpg"
            };

            var mockService = new Mock<IRecipeService>();
            mockService.Setup(service => service.GetRecipeById(recipeId)).ReturnsAsync(existingRecipe);
            var recipesController = new RecipesApiController(mockService.Object);
            var result = recipesController.Put(recipeId, recipeRequest);
            Assert.Equal(result, true);
        }
        [Fact]
        public void TestGetRecipeByCategory()
        {
            var categoryId = 1;
            var recipesInCategory = new List<Recipe>
            {
                new Recipe{
                 recipeId = 1,
                    title = "Scrambled Eggs",
                    description = "A classic breakfast dish",
                    instructions = "1. Whisk eggs and milk together in a bowl. 2. Melt butter in a nonstick skillet over medium heat. 3. Pour egg mixture into skillet and cook, stirring occasionally, until eggs are set but still moist, about 3-5 minutes. 4. Season with salt and pepper to taste.",
                    preparation_time = 10,
                    servings = 2,
                    imageUrl= "https://www.allrecipes.com/thmb/HcdHiuwiNOIlOISGKGTI0KxhR7E=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/JF_241160_CreamyCottageCheeseScrambled_4x3_12902-619d00dc88594ea9b8ed884a108db16d.jpg"
                },
                new Recipe
                {
                    recipeId = 2,
                    title = "Spaghetti Bolognese",
                    description = "A delicious Italian dinner",
                    instructions = "1. Cook spaghetti according to package instructions. 2. Heat oil in a large skillet over medium-high heat. 3. Add ground beef and cook until browned, stirring occasionally. 4. Add onion, carrot, and celery and cook until vegetables are softened. 5. Add garlic and cook until fragrant. 6. Add tomato paste, crushed tomatoes, and beef broth and bring to a simmer. 7. Reduce heat and let simmer until sauce has thickened, about 20-30 minutes. 8. Season with salt and pepper to taste. 9. Serve over spaghetti.",
                    preparation_time = 45,
                    servings = 4,
                    imageUrl= "https://www.slimmingeats.com/blog/wp-content/uploads/2010/04/spaghetti-bolognese-36-735x735.jpg"
                },
                new Recipe
                {
                    recipeId = 3,
                    title = "Chocolate Chip Cookies",
                    description = "A classic American dessert",
                    instructions = "1. Preheat oven to 375°F. 2. Cream together butter, white sugar, and brown sugar until smooth. 3. Beat in eggs one at a time, then stir in vanilla. 4. Dissolve baking soda in hot water and add to batter. 5. Stir in flour, chocolate chips, and nuts. 6. Drop by large spoonfuls onto ungreased pans. 7. Bake for about 10 minutes or until edges are nicely browned.",
                    preparation_time = 30,
                    servings = 24,
                    imageUrl= "https://images.aws.nestle.recipes/original/5b069c3ed2feea79377014f6766fcd49_Original_NTH_Chocolate_Chip_Cookie.jpg"
                }
            };
            var mockService = new Mock<IRecipeService>();
            mockService.Setup(service => service.GetRecipesByCategory(categoryId)).ReturnsAsync(recipesInCategory);
            var recipesController = new RecipesApiController(mockService.Object);
            var result = recipesController.GetRecipeByCategory(categoryId);
            var recipes = Assert.IsAssignableFrom<IEnumerable<RecipeResponse>>(result);
        }

        [Fact]
        public void TestGetRecipeByCategoryNonExistentCategoryReturnsEmptyList()
        {
            var categoryId = 999;
            var mockService = new Mock<IRecipeService>();
            mockService.Setup(service => service.GetRecipesByCategory(categoryId)).ReturnsAsync(new List<Recipe>());
            var recipesController = new RecipesApiController(mockService.Object);
            var result = recipesController.GetRecipeByCategory(categoryId);
            var recipes = Assert.IsAssignableFrom<IEnumerable<RecipeResponse>>(result);
            Assert.Empty(recipes);
        }

    }
}
