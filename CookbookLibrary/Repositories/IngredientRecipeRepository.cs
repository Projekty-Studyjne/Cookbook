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
    public class IngredientRecipeRepository : IIngredientRecipeRepository, IDisposable
    {
        private CookbookDbContext context;

        public IngredientRecipeRepository(CookbookDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<IngredientRecipe> GetIngredientRecipes()
        {
            return context.IngredientRecipes.ToList();
        }

        public IngredientRecipe GetIngredientRecipeById(int id)
        {
            return context.IngredientRecipes.Find(id);
        }

        public void InsertIngredientRecipe(IngredientRecipe ingredientRecipe)
        {
            context.IngredientRecipes.Add(ingredientRecipe);
        }

        public void DeleteIngredientRecipe(int ingredientRecipeID)
        {
            IngredientRecipe ingredientRecipe = context.IngredientRecipes.Find(ingredientRecipeID);
            context.IngredientRecipes.Remove(ingredientRecipe);
        }

        public void UpdateIngredientRecipe(IngredientRecipe ingredientRecipe)
        {
            context.Entry(ingredientRecipe).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
