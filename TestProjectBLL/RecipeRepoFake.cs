using CookbookLibrary;
using CookbookLibrary.Entities;
using CookbookLibrary.Repositories;
using CookbookLibrary.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectBLL
{
    internal class RecipeRepoFake : RecipeRepository
    {
        private List<Recipe> recipes = new List<Recipe>();

        public new Task<IEnumerable<Recipe>> GetAsync(
            Expression<Func<Recipe, bool>> filter = null,
            Func<IQueryable<Recipe>, IOrderedQueryable<Recipe>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<Recipe> query = recipes.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return Task.FromResult<IEnumerable<Recipe>>(orderBy(query).ToList());
            }
            else
            {
                return Task.FromResult<IEnumerable<Recipe>>(query.ToList());
            }
        }

        public new Recipe GetByID(object id)
        {
            return recipes.FirstOrDefault(e => e.recipeId == (int)id);
        }

        public new void Insert(Recipe recipe)
        {
            recipes.Add(recipe);
        }

        public new void Delete(object recipeId)
        {
            Recipe recipe = recipes.Find(s=>s.recipeId== (int)recipeId);
            recipes.Remove(recipe);
        }

        public new void Update(Recipe recipe)
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

        public RecipeRepoFake(CookbookDbContext context) : base(context)
        {
        }

        protected virtual void Dispose(bool disposing)
        {
            //hehe
        }

        public new void Dispose()
        {
            //hehe
        }

        public new void Delete(Recipe entityToDelete)
        {
            Recipe recipe = recipes.Find(s => s.recipeId == entityToDelete.recipeId);
            recipes.Remove(recipe);
        }
    }
}
