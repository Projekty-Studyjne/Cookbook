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
    public class CategoryRecipeRepository : ICategoryRecipeRepository, IDisposable
    {
        private CookbookDbContext context;

        public CategoryRecipeRepository(CookbookDbContext context)
        {
            context = context;
        }

        public IEnumerable<CategoryRecipe> GetCategoryRecipes()
        {
            return context.CategoryRecipes.ToList();
        }

        public CategoryRecipe GetCategoryRecipeById(int id)
        {
            return context.CategoryRecipes.Find(id);
        }

        public void InsertCategoryRecipe(CategoryRecipe categoryRecipe)
        {
            context.CategoryRecipes.Add(categoryRecipe);
        }

        public void DeleteCategoryRecipe(int categoryRecipeId)
        {
            CategoryRecipe categoryRecipe = context.CategoryRecipes.Find(categoryRecipeId);
            context.CategoryRecipes.Remove(categoryRecipe);
        }

        public void UpdateCategoryRecipe(CategoryRecipe categoryRecipe)
        {
            context.Entry(categoryRecipe).State = EntityState.Modified;
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
