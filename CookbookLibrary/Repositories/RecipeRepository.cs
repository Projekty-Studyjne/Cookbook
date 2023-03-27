using CookbookLibrary.Entities;
using CookbookLibrary.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookLibrary.Repositories
{
    public class RecipeRepository : IRecipeRepository, IDisposable
    {
        private CookbookDbContext context;

        public RecipeRepository(CookbookDbContext context)
        {
            context = context;
        }

        public IEnumerable<Recipe> GetRecipes()
        {
            return context.Recipes.ToList();
        }

        public Recipe GetRecipeById(int id)
        {
            return context.Recipes.Find(id);
        }

        public void InsertRecipe(Recipe recipe)
        {
            context.Recipes.Add(recipe);
        }

        public void DeleteRecipe(int recipeId)
        {
            Recipe recipe = context.Recipes.Find(recipeId);
            context.Recipes.Remove(recipe);
        }

        public void UpdateRecipe(Recipe recipe)
        {
            context.Entry(recipe).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
