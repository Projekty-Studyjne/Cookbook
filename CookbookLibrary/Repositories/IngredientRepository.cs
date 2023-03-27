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
    public class IngredientRepository : IIngredientRepository, IDisposable
    {
        private CookbookDbContext context;

        public IngredientRepository(CookbookDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Ingredient> GetIngredients()
        {
            return context.Ingredients.ToList();
        }

        public Ingredient GetIngredientById(int id)
        {
            return context.Ingredients.Find(id);
        }

        public void InsertIngredient(Ingredient ingredient)
        {
            context.Ingredients.Add(ingredient);
        }

        public void DeleteIngredient(int ingredientID)
        {
            Ingredient ingredient = context.Ingredients.Find(ingredientID);
            context.Ingredients.Remove(ingredient);
        }

        public void UpdateIngredient(Ingredient ingredient)
        {
            context.Entry(ingredient).State = EntityState.Modified;
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
