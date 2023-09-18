using CookbookBLL.Interfaces;
using CookbookLibrary.Entities;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestControlersMVC
{
    internal class RecipeBLLMock : IRecipeService
    {
        public Task Add(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public bool isWorking { get; set; }
        public async Task AddCategoryToRecipe(int recipeId, Category category)
        {
            isWorking = true;
        }

        public Task Delete(int recipeId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Recipe>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Recipe> GetRecipeById(int recipeId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Recipe>> GetRecipesByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Recipe>> GetRecipesByIngredient(int ingredientId)
        {
            throw new NotImplementedException();
        }

        public Task Update(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Recipe>> GetRecipesByIngredientName(string ingredientName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Recipe>> GetRecipesByName(string name)
        {
            throw new NotImplementedException();
        }

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public Task AddCategoryToRecipe(int recipeId, int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
