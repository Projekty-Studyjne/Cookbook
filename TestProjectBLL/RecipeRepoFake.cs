using CookbookLibrary.Entities;
using CookbookLibrary.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectBLL
{
    internal class RecipeRepoFake : IRecipeRepository
    {
        private List<Recipe> recipes = new List<Recipe>();

        public IEnumerable<Recipe> GetRecipes()
        {
            return this.recipes;
        }

        public Recipe GetRecipeById(int id)
        {
            return recipes.Find(s=>s.recipeId==id);
        }

        public void InsertRecipe(Recipe recipe)
        {
            recipes.Add(recipe);
        }

        public void DeleteRecipe(int recipeId)
        {
            Recipe recipe = recipes.Find(s=>s.recipeId==recipeId);
            recipes.Remove(recipe);
        }

        public void UpdateRecipe(Recipe recipe)
        {
            int index = this.recipes.FindIndex(s => s.recipeId == recipe.recipeId);
            if (index != -1)
                recipes[index] = recipe;
        }

        public void Save()
        {
            //hehe
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            //hehe
        }

        public void Dispose()
        {
            //hehe
        }
    }
}
