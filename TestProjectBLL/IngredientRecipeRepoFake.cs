using CookbookLibrary.Entities;
using CookbookLibrary.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectBLL
{
    public class IngredientRecipeRepoFake : IGenericRepository<IngredientRecipe>
    {
        private List<IngredientRecipe> ingredientRecipes = new List<IngredientRecipe>();
        public void Delete(object id)
        {
            IngredientRecipe ingredientRecipe = ingredientRecipes.Find(s => s.ingredientId == (int)id);
            ingredientRecipes.Remove(ingredientRecipe);
        }

        public void Delete(IngredientRecipe entityToDelete)
        {
            ingredientRecipes.Remove(entityToDelete);
        }

        public Task<IEnumerable<IngredientRecipe>> GetAsync(Expression<Func<IngredientRecipe, bool>> filter = null, Func<IQueryable<IngredientRecipe>, IOrderedQueryable<IngredientRecipe>> orderBy = null, string includeProperties = "")
        {
            IQueryable<IngredientRecipe> query = ingredientRecipes.AsQueryable();

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
                return Task.FromResult<IEnumerable<IngredientRecipe>>(orderBy(query).ToList());
            }
            else
            {
                return Task.FromResult<IEnumerable<IngredientRecipe>>(query.ToList());
            }
        }

        public IngredientRecipe GetByID(object id)
        {
            return ingredientRecipes.FirstOrDefault(e => e.ingredientId == (int)id);
        }

        public void Insert(IngredientRecipe entity)
        {
            ingredientRecipes.Add(entity);
        }

        public void Update(IngredientRecipe entityToUpdate)
        {
            int index = this.ingredientRecipes.FindIndex(s => s.ingredientId == entityToUpdate.ingredientId && s.recipeId == entityToUpdate.recipeId);
            if (index != -1)
                ingredientRecipes[index] = entityToUpdate;
        }
    }
}
